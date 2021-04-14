using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using SmartApartmentData.Framework.ElasticSearch.Repository;
using SmartApartmentData.Framework.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SmartApartmentData.Framework
{
    public static class Config
    {
        public static void AddCustomFramworkServices(this IServiceCollection services)
        {
            var settings = Config.GetElasticSearchConfigs();
            var connectionsetting = new ConnectionSettings(new Uri(settings.URL));
            connectionsetting.BasicAuthentication(settings.Username, settings.PWD);

            services.AddSingleton<IElasticClient>(x => new ElasticClient(connectionsetting));
            services.AddTransient<ISearchRepository, SearchRepository>();
            services.AddTransient<ISearchService, SearchService>();

        }
        public static ElasticSearchSettings GetElasticSearchConfigs()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: false);

            IConfiguration config = builder.Build();

            var settings = config.GetSection("KeySettings").GetSection("ElasticSearch")
                .Get<ElasticSearchSettings>();

            return settings;
        }
    }
    public class ElasticSearchSettings
    {
        public string URL { get; set; }
        public string Username { get; set; }
        public string PWD { get; set; }
    }
}
