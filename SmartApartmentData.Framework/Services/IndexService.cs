using Nest;
using SmartApartmentData.Framework.ElasticSearch.Documents;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartApartmentData.Framework.Services
{
    public class IndexService : IIndexService
    {
        IElasticClient _client;
        public IndexService(IElasticClient client)
        {
            _client = client;
        }

        public void CleanUpIndex(string[] indicesName)
        {
            foreach (var name in indicesName)
            { var x = _client.Indices.Delete(name); }
        }
        public void CreateIndex<T>(string name, string alias) where T : class
        {
            var createPropertyIndexResponse = _client.Indices.Create(name,
                x => x
                .Map<T>(p => p.AutoMap())
                .Aliases(p => p.Alias(alias))
                .Settings(p => p.Analysis(a => a.Analyzers(z => z.Snowball("snowball", sa => sa.Language(SnowballLanguage.English)))))
                );
        }
        public void IndexDocumnets<T>(List<T> documents, string index) where T : class
        {
            var step = 1000;
            for (var i = 0; i < documents.Count; i += step)
                _client.IndexMany<T>(documents.Skip(i).Take(step), index);

        }
    }
}
