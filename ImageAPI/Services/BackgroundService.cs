using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using ImageAPI.Interfaces;

namespace ImageAPI.Services
{
    public class CachingServiceLoaderBackgroundService : IHostedService
    {
        IImageCachingService _cachingService;
        int _minutes;

        public CachingServiceLoaderBackgroundService(int minutes, IImageCachingService cachingService)
        {
            _minutes = minutes;
            _cachingService = cachingService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                await _cachingService.Load();

                Console.WriteLine("loaded! " + DateTime.Now.ToString());

                await Task.Delay(TimeSpan.FromMinutes(_minutes), cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
