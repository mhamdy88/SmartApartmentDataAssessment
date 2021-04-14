using SmartApartmentData.Framework.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartApartmentData.Framework.ElasticSearch.Repository
{
    public interface ISearchRepository
    {
        SearchResult Search(string query, string markets = null, int limit = 25);
    }
}
