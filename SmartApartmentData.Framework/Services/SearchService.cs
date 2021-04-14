using Nest;
using SmartApartmentData.Framework.ElasticSearch.Repository;
using SmartApartmentData.Framework.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartApartmentData.Framework.Services
{
    public class SearchService : ISearchService
    {
        private ISearchRepository _searchRepository;
        public SearchService(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }
        public SearchResult Search(string query,string markets = null, int limit = 25)
        {
            return _searchRepository.Search(query, markets, limit);
        }
    }
}
