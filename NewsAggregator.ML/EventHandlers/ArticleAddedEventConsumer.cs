﻿using MassTransit;
using NewsAggregator.Core.Domains.Articles.Events;
using NewsAggregator.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.ML.EventHandlers
{
    public class ArticleAddedEventConsumer : IConsumer<ArticleAddedEvent>
    {
        private readonly IDataSourceCommandRepository _dataSourceCommandRepository;

        public ArticleAddedEventConsumer(IDataSourceCommandRepository dataSourceCommandRepository)
        {
            _dataSourceCommandRepository = dataSourceCommandRepository;
        }

        public async Task Consume(ConsumeContext<ArticleAddedEvent> context)
        {
            var datasource = await _dataSourceCommandRepository.Get(context.Message.DataSourceId, CancellationToken.None);
            datasource.AddArticle(context.Message.PublishDate);
            await _dataSourceCommandRepository.Update(new[] { datasource }, CancellationToken.None);
            await _dataSourceCommandRepository.SaveChanges(CancellationToken.None);
        }
    }
}
