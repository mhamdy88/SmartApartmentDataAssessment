using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using SmartApartmentData.Framework.ElasticSearch.Repository;
using SmartApartmentData.Framework.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartApartmentData.Framework
{
    public static class Config
    {
        private static string _elasticURL = "http://localhost:9200";
        public static ContainerBuilder Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ElasticClient>().As<IElasticClient>()
                .SingleInstance().WithParameter("connectionSettings", new ConnectionSettings(new Uri(_elasticURL)));

            builder.RegisterType<SearchService>().As<ISearchService>();
            return builder;
        }
        public static void AddCustomFramworkServices(this IServiceCollection services)
        {
            //services.AddSingleton<IElasticClient, ElasticClient>();
            services.AddSingleton<IElasticClient>(x => new ElasticClient(new ConnectionSettings(new Uri(_elasticURL))));
            services.AddTransient<ISearchRepository, SearchRepository>();
            services.AddTransient<ISearchService, SearchService>();

        }
    }
}
