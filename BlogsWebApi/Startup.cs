﻿namespace BlogsWebApi
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Configuration.Builders;
    using Configuration.InternalServices;
    using Shared.Settings;

    public class Startup
    {
        public ISettings Settings { get; }

        public ILog Log { get; }

        public Startup(ISettings settings, ILog log)
        {
            Settings = settings;
            Log = log;
        }

        public void ConfigureServices(IServiceCollection services) => services.ConfigureServices(Settings);

        public void Configure(IApplicationBuilder app, IHostingEnvironment env) => app.ConfigureApplication(env, Log);
    }
}