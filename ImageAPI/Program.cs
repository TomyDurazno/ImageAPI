using ImageAPI.Interfaces;
using ImageAPI.Models;
using ImageAPI.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ImageAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureServices((hostContext,services) =>
                {
                    var settings = hostContext.Configuration.GetSection("Settings").Get<Settings>();
                    services.AddHostedService(sp => new CachingServiceLoaderBackgroundService(settings.backgroundServiceInterval, sp.GetService<IImageCachingService>()));
                });
    }
}
