namespace Configuration.Middleware
{
    using Microsoft.Extensions.DependencyInjection;
    using AutoMapper;
    using Mappings;

    internal static class AutoMapperExtension
    {
        internal static void ConfigureAutoMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(configure => { configure.AddProfile(new MappingProfile()); });

            var mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}