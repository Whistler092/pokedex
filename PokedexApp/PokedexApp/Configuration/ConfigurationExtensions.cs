using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Pokedex.Core.Business;
using Pokedex.Core.Business.Interfaces;
using Pokedex.Core.Core;
using Pokedex.Core.Core.Interfaces;
using Pokedex.Core.Integrations;
using Pokedex.Core.Integrations.Interfaces;
using System;
using System.IO;
using System.Reflection;

namespace PokedexApp.Configuration
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection AddEntityFramework
            (this IServiceCollection services)
        {
            services.AddDbContext<PokedexContext>(options =>
                options.UseInMemoryDatabase(databaseName: "Demo"));
            return services;
        }

        public static IServiceCollection AddRepositories
            (this IServiceCollection services)
        {
            services.AddTransient<ITrainerBusiness, TrainerBusiness>();
            services.AddTransient<IPokeBusiness, PokeBusiness>();

            services.AddHttpClient();

            services.AddTransient<ITrainerData, TrainerData>();
            services.AddTransient<IPokeAPIIntegration, PokeAPIIntegration>();
            
            return services;
        }

        public static IServiceCollection AddSwagger
            (this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Demo API",
                    Description = "Demo",
                    Contact = new OpenApiContact
                    {
                        Name = "Demo",
                        Url = new Uri("https://ramirobedoya.me")
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            return services;
        }

        public static IApplicationBuilder ConfigureSwagger
             (this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo API v1");
            });

            return app;
        }
    }
}
