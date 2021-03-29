﻿using Microsoft.Extensions.Options;
using NewsAggregator.Domain;
using NewsAggregator.ML.Infrastructures.Bus;
using NewsAggregator.ML.Infrastructures.Jobs;
using NewsAggregator.ML.Infrastructures.Locks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.ML.Jobs
{
    /*
    public class ProcessDomainEventsJob : BaseJob<DomainEventNotification>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IDistributedLock _distributedLock;

        public ProcessDomainEventsJob(
            IMessageBroker messageBroker, 
            IOptions<NewsAggregatorMLOptions> options, 
            IServiceProvider serviceProvider, 
            IDistributedLock distributedLock) : base(messageBroker, options)
        {
            _serviceProvider = serviceProvider;
            _distributedLock = distributedLock;
        }

        protected override string QueueName => Constants.QueueNames.DomainEvents;

        protected override async Task ProcessMessage(DomainEventNotification notification, CancellationToken cancellationToken)
        {
            string lockName = $"domainevt:{notification.Id}";
            if (!await _distributedLock.TryAcquireLock(lockName, cancellationToken))
            {
                return;
            }

            try
            {
                // TODO : Add transaction !!!
                var assm = Options.ApplicationAssembly;
                var lstTask = new List<Task>();
                foreach (var record in notification.Evts)
                {
                    var genericType = assm.GetType(record.Type);
                    var messageBrokerType = typeof(IEventHandler<>).MakeGenericType(genericType);
                    var lst = (IEnumerable<object>)_serviceProvider.GetService(typeof(IEnumerable<>).MakeGenericType(messageBrokerType));
                    var message = JsonConvert.DeserializeObject(record.Content, genericType);
                    foreach (var r in lst)
                    {
                        await (Task)messageBrokerType.GetMethod("Handle").Invoke(r, new object[] { message, cancellationToken });
                    }
                }
            }
            finally
            {
                await _distributedLock.ReleaseLock(lockName, cancellationToken);
            }
        }
    }
    */
}
