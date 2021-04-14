using SmartApartmentData.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartApartmentData.WebAPI.Models
{
    public class SearchResponseModel
    {
        public bool Success { get; set; }
        public long Total { get; set; }
        public SearchResult SearchResult { get; set; }
        public string Message { get; set; }
    }
}
