﻿using NewsAggregator.Core.Domains.Articles.Events;
using NewsAggregator.Core.Exceptions;
using NewsAggregator.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewsAggregator.Core.Domains.Articles
{
    public class ArticleAggregate : BaseAggregate
    {
        private ArticleAggregate() 
        {
            ArticleLikeLst = new List<ArticleLike>();
        }

        public string ExternalId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public string Language { get; set; }
        public string DataSourceId { get; set; }
        public DateTimeOffset PublishDate { get; set; }
        public int NbViews { get; set; }
        public int NbLikes { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public ICollection<ArticleLike> ArticleLikeLst { get; set; }

        public static ArticleAggregate Create(string externalId, string title, string summary, string content, string language, string datasourceId, DateTimeOffset publishDate)
        {
            var result = new ArticleAggregate();
            var evt = new ArticleAddedEvent(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), 0, externalId, title, summary, content, language, datasourceId, publishDate);
            result.Handle(evt);
            result.DomainEvts.Add(evt);
            return result;
        }

        public void View(string userId, string sessionId)
        {
            var evt = new ArticleViewedEvent(Guid.NewGuid().ToString(), Id, Version + 1, Language, userId, sessionId, DateTime.UtcNow);
            Handle(evt);
            DomainEvts.Add(evt);
        }

        public void Like(string userId, string sessionId)
        {
            var evt = new ArticleLikedEvent(Guid.NewGuid().ToString(), Id, Version + 1, Language, userId, sessionId, DateTime.UtcNow);
            Handle(evt);
            DomainEvts.Add(evt);
        }

        public void Unlike(string userId, string sessionId)
        {
            var evt = new ArticleUnlikedEvent(Guid.NewGuid().ToString(), Id, Version + 1, Language, userId, sessionId, DateTime.UtcNow);
            Handle(evt);
            DomainEvts.Add(evt);
        }

        protected override void Handle(dynamic evt)
        {
            Handle(evt);
        }

        private void Handle(ArticleAddedEvent evt)
        {
            Id = evt.AggregateId;
            ExternalId = evt.ExternalId;
            Title = evt.Title;
            Summary = evt.Summary;
            Content = evt.Content;
            Language = evt.Language;
            DataSourceId = evt.DataSourceId;
            PublishDate = evt.PublishDate;
            Version = evt.Version;
        }

        private void Handle(ArticleViewedEvent evt)
        {
            NbViews++;
            Version = evt.Version;
            UpdateDateTime = evt.ActionDateTime;
        }

        private void Handle(ArticleLikedEvent evt)
        {
            if (ArticleLikeLst.Any(l => l.UserId == evt.UserId))
            {
                throw new DomainException(Global.ArticleAlreadyLikedByTheUser);
            }

            NbLikes++;
            Version = evt.Version;
            UpdateDateTime = evt.ActionDateTime;
            ArticleLikeLst.Add(ArticleLike.Create(evt.UserId, evt.ActionDateTime));
        }

        private void Handle(ArticleUnlikedEvent evt)
        {
            var articleLike = ArticleLikeLst.FirstOrDefault(l => l.UserId == evt.UserId);
            if (articleLike == null)
            {
                throw new DomainException(Global.ArticleNotLikedByTheUser);
            }

            NbLikes--;
            Version = evt.Version;
            UpdateDateTime = evt.ActionDateTime;
            ArticleLikeLst.Remove(articleLike);
        }
    }
}
