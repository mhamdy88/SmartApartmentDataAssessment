using Microsoft.Extensions.Configuration;
using SmartApartmentData.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SmartApartmentData.IndexClient
{
    static class _Config
    {
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
    
}
