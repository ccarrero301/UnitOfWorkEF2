namespace BlogsWebApi.Configuration.Middleware
{
    using Microsoft.Extensions.DependencyInjection;
    using AutoMapper;
    using Mappings;

    public static class AutoMapperExtension
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(configure =>
            {
                configure.AddProfile(new MappingProfile());
            });

            var mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}