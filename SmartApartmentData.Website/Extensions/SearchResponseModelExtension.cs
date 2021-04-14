using SmartApartmentData.Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartApartmentData.Website.Extensions
{
    public static class SearchResponseModelExtension
    {
        public static List<SearchResultItemViewModel> ToSearchResultItemViewModel(this SearchResponseModel model)
        {
            var res = new List<SearchResultItemViewModel>();
            if (model.Success && model.SearchResult.SearchResultItems.Count > 0)
            {
                foreach (var item in model.SearchResult.SearchResultItems)
                {
                    res.Add(new SearchResultItemViewModel()
                    {
                        Id = item.Id,
                        Text = item.Name,
                        Type = item.Type
                    });
                }
            }
            return res;
        }
    }
}
