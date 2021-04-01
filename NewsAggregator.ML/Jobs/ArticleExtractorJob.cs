﻿using Hangfire;
using Microsoft.Extensions.Logging;
using NewsAggregator.Core.Domains.Articles;
using NewsAggregator.Core.Domains.DataSources;
using NewsAggregator.Core.Repositories;
using NewsAggregator.ML.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml;

namespace NewsAggregator.ML.Jobs
{
    public class ArticleExtractorJob : IArticleExtractorJob
    {
        private readonly IDataSourceCommandRepository _datasourceCommandRepository;
        private readonly IArticleCommandRepository _articleRepository;
        private readonly IArticleManager _articleManager;
        private readonly ILogger<ArticleExtractorJob> _logger;

        public ArticleExtractorJob(
            IDataSourceCommandRepository datasourceCommandRepository,
            IArticleCommandRepository articleRepository,
            IArticleManager articleManager,
            ILogger<ArticleExtractorJob> logger)
        {
            _datasourceCommandRepository = datasourceCommandRepository;
            _articleRepository = articleRepository;
            _articleManager = articleManager;
            _logger = logger;
        }

        [DisableConcurrentExecution(5 * 60)]
        public async Task Run(CancellationToken cancellationToken)
        {
            var datasources = await _datasourceCommandRepository.GetAll(cancellationToken);
            foreach (var datasource in datasources)
            {
                try
                {
                    await ExtractArticlesFromFeed(datasource, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                }
            }

            await _datasourceCommandRepository.Update(datasources, cancellationToken);
            await _datasourceCommandRepository.SaveChanges(cancellationToken);
        }

        private async Task ExtractArticlesFromFeed(DataSourceAggregate datasource, CancellationToken cancellationToken)
        {
            var reader = XmlReader.Create(datasource.Url);
            var feed = SyndicationFeed.Load(reader);
            reader.Close();
            var result = new List<ArticleAggregate>();
            foreach (var item in feed.Items.OrderBy(i => i.PublishDate))
            {
                if (!datasource.IsArticleExtracted(item.PublishDate))
                {
                    var article = ArticleAggregate.Create(item.Id, item.Title.Text, item.Summary.Text, null, "en", datasource.Id, item.PublishDate);
                    result.Add(article);
                }
            }

            if (!result.Any())
            {
                return;
            }

            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                foreach (var article in result)
                {
                    await _articleRepository.Add(article, cancellationToken);
                }

                await _articleRepository.SaveChanges(cancellationToken);
                transactionScope.Complete();
            }

            await _articleManager.TrainArticles(result.Select(r => r.Language).Distinct(), cancellationToken);
            var lastArticle = result.OrderByDescending(a => a.PublishDate).FirstOrDefault();
            if (lastArticle != null)
            {
                datasource.AddHistory(lastArticle.PublishDate, result.Count());
            }
        }
    }
}
