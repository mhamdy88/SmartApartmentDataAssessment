using Nest;
using SmartApartmentData.Framework.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartApartmentData.Framework.Extensions
{
    public static class HitsMappingExtentions
    {
        public static SearchResult ToSearchResult(this IReadOnlyCollection<IHit<dynamic>> hits)
        {
            var res = new SearchResult();
            if (hits?.Count > 0)
            {
                res.SearchResultItems = new List<SearchResultItem>();
                foreach (var item in hits)
                {
                    res.SearchResultItems.Add(new SearchResultItem
                    {
                        Name = item.Source["name"],
                        Type = item.Index,
                        Id = item.Id,
                        Source = item.Source
                    });
                }
            }
            return res;
        }
    }
}
