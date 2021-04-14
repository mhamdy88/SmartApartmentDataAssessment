using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartApartmentData.WebAPI.Models
{
    public class SearchRequestModel
    {
        public string Query { get; set; }
        public string Markets { get; set; }
        public int Limit { get; set; }
    }
}
