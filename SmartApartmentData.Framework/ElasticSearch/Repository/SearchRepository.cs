using Nest;
using SmartApartmentData.Framework.Extensions;
using SmartApartmentData.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartApartmentData.Framework.ElasticSearch.Repository
{
    public class SearchRepository : ISearchRepository
    {

        IElasticClient _client;
        public SearchRepository(IElasticClient client)
        {
            _client = client;
        }
        public SearchResult Search(string query, string markets = null, int limit = 25)
        {
            var boolQuery = new BoolQuery();
            boolQuery.Must = new QueryContainer[] { new MultiMatchQuery
                {
                    Query = query,
                    Fields = "*"
                }
            };
            if (!string.IsNullOrEmpty(markets?.Trim()))
            {
                boolQuery.Filter = new QueryContainer[] { new MatchQuery
                {   Field = "market",
                    Query =  markets
                }};
            }
            var esResponse = _client.Search<dynamic>(s => s.Index("smartapartmentdata").Size(limit).Query(_ => boolQuery));

            return esResponse?.Hits?.ToSearchResult();
        }
    }
}
