﻿namespace BlogsWebApi.Configuration.Builders
{
    using Microsoft.Extensions.DependencyInjection;
    using Middlewares;
    using InternalServices;

    public static class ServicesBuilder
    {
        public static void ConfigureServices(this IServiceCollection services, ISettings settings)
        {
            services.ConfigureAuthentication(settings);

            services.ConfigureAuthorization(settings);

            services.InjectUnitOfWork(settings);

            services.InjectServices();

            services.ConfigureSwagger();

            services.ConfigureMvc();
        }
    }
}