﻿namespace UnitOfWorkWebApi.Configuration.Middlewares
{
    using System.Text;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Requirements;
    using InternalServices;

    public static class AuthenticationAndAuthorizationExtension
    {
        public static void ConfigureAuthentication(this IServiceCollection services, ISettings settings)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = settings.JwtIssuer,
                        ValidAudience = settings.JwtAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(settings.JwtGeneratorKey))
                    };
                });
        }

        public static void ConfigureAuthorization(this IServiceCollection services, ISettings settings)
        {
            services.AddAuthorization(
                options =>
                {
                    options.AddPolicy("AuthenticatedUser", policy => { policy.RequireAuthenticatedUser(); });

                    options.AddPolicy("B2B",
                        policy => { policy.Requirements.Add(new B2BRequirement($"Bearer {settings.B2BSecret}")); });
                });
        }
    }
}