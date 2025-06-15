using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectBase.Services.BackgroundServices
{
    public class QueuedBackgroundService : BackgroundService
    {
        private readonly ConcurrentQueue<Func<CancellationToken, Task>> _taskQueue = new();
        private readonly SemaphoreSlim _signal = new(0);

        public void QueueWorkItemAsync(Func<CancellationToken, Task> workItem)
        {
            if (workItem == null)
                throw new ArgumentNullException(nameof(workItem));

            _taskQueue.Enqueue(workItem);
            _signal.Release();
        }

        public void EnqueueTask(Func<Task> task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            _taskQueue.Enqueue(async (cancellationToken) => await task());
            _signal.Release();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _signal.WaitAsync(stoppingToken);

                if (_taskQueue.TryDequeue(out var workItem))
                {
                    try
                    {
                        await workItem(stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        // Handle task exception
                        Console.WriteLine($"Task execution failed: {ex.Message}");
                    }
                }
            }
        }
    }
}