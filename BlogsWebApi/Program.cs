namespace BlogsWebApi
{
    using System;
    using System.IO;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Configuration;
    using Configuration.InternalServices;
    using Shared.Settings;
    
    public class Program
    {
        public static void Main(string[] args) => CreateWebHostBuilder(args).Build().Run();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{environment}.json", false, true).Build();

            return ConfigureStartUpServices(args, configuration)
                .UseStartup<Startup>();
        }

        private static IWebHostBuilder ConfigureStartUpServices(string[] args, IConfiguration configuration) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddSingleton<ISettings>(provider => new ApplicationSettings(configuration));
                    services.AddSingleton<ILog, TextFileLog>();
                });
    }
}