using SmartApartmentData.Framework.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartApartmentData.Framework.Services
{
    public interface ISearchService
    {
        SearchResult Search(string query, string markets = null, int limit = 25);
    }
}
