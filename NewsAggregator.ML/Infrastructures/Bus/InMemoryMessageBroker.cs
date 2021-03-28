﻿using NewsAggregator.Core.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.ML.Infrastructures.Bus
{
    public class InMemoryMessageBroker : IMessageBroker
    {
        public ConcurrentDictionary<string, BlockingCollection<string>> _workingQueues;
        public ConcurrentBag<ScheduledMessage> _scheduledMessages;

        public InMemoryMessageBroker()
        {
            _workingQueues = new ConcurrentDictionary<string, BlockingCollection<string>>();
            _scheduledMessages = new ConcurrentBag<ScheduledMessage>();
        }

        public Task Queue(string queueName, object msg, CancellationToken token)
        {
            return Queue(queueName, JsonConvert.SerializeObject(msg), token);
        }

        public Task Queue(string queueName, string serializedMessage, CancellationToken token)
        {
            if (!_workingQueues.ContainsKey(queueName))
            {
                _workingQueues.TryAdd(queueName, new BlockingCollection<string> { serializedMessage });
                return Task.CompletedTask;
            }

            _workingQueues[queueName].Add(serializedMessage);
            return Task.CompletedTask;
        }

        public Task<T> Dequeue<T>(string queueName, CancellationToken cancellationToken) where T : class
        {
            if (!_workingQueues.ContainsKey(queueName))
            {
                return Task.FromResult((T)null);
            }

            if (_workingQueues[queueName].TryTake(out string msg, 100, cancellationToken))
            {
                return Task.FromResult(JsonConvert.DeserializeObject<T>(msg));
            }

            return Task.FromResult((T)null);
        }

        public Task QueueScheduledMessage(string queueName, object msg, DateTime elapsedTime, CancellationToken token)
        {
            _scheduledMessages.Add(new ScheduledMessage
            {
                SerializedContent = JsonConvert.SerializeObject(msg),
                ElapsedTime = elapsedTime,
                QueueName = queueName
            });
            return Task.CompletedTask;
        }

        public Task<List<ScheduledMessage>> DequeueScheduledMessage(CancellationToken token)
        {
            var scheduledMessages = _scheduledMessages.Where(_ => _.ElapsedTime <= DateTime.UtcNow).ToList();
            for (int i = 0; i < scheduledMessages.Count; i++)
            {
                var record = scheduledMessages[i];
                _scheduledMessages.Remove(record);
            }

            return Task.FromResult(scheduledMessages);
        }

        public void Dispose()
        {
        }
    }
}
