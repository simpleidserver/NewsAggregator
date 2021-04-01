﻿using System;
using System.Diagnostics;

namespace NewsAggregator.Core.Domains.Feeds.Events
{
    [DebuggerDisplay("Subscribe to the datasource in the feed")]
    public class FeedDataSourceSubscribedEvent : DomainEvent
    {
        public FeedDataSourceSubscribedEvent(string id, string aggregateId, int version, string userId, string datasourceId, DateTime createDateTime) : base(id, aggregateId, version)
        {
            UserId = userId;
            DataSourceId = datasourceId;
            CreateDateTime = createDateTime;
        }

        public string UserId { get; set; }
        public string DataSourceId { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}
