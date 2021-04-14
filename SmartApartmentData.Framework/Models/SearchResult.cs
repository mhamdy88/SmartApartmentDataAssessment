using System;
using System.Collections.Generic;
using System.Text;

namespace SmartApartmentData.Framework.Models
{
    public class SearchResult
    {
        public List<SearchResultItem> SearchResultItems { get; set; } = new List<SearchResultItem>();
    }
    public class SearchResultItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Dictionary<string, object> Source { get; set; }
    }
}
