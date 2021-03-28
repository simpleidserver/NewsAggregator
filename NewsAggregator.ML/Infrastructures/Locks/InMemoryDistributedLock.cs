﻿using NewsAggregator.Core.Extensions;
using NewsAggregator.ML.Extensions;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAggregator.ML.Infrastructures.Locks
{
    public class InMemoryDistributedLock : IDistributedLock
    {
        private ConcurrentBag<string> _locks;

        public InMemoryDistributedLock()
        {
            _locks = new ConcurrentBag<string>();
        }

        public Task<bool> TryAcquireLock(string id, CancellationToken token)
        {
            if (_locks.Contains(id))
            {
                return Task.FromResult(false);
            }

            _locks.Add(id);
            return Task.FromResult(true);
        }

        public Task ReleaseLock(string id, CancellationToken token)
        {
            _locks.Remove(id);
            return Task.CompletedTask;
        }
    }
}
