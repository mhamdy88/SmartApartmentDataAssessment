
using Microsoft.AspNetCore.Mvc;
using SmartApartmentData.Framework.Models;
using SmartApartmentData.Framework.Services;
using SmartApartmentData.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartApartmentData.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private ISearchService _searchService;
        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }
        [HttpPost]
        public ActionResult Search([FromBody] SearchRequestModel request)
        {
            SearchResponseModel model;
            try
            {
                var resposnse = _searchService.Search(request.Query, request.Markets, request.Limit);
                model = new SearchResponseModel()
                {
                    SearchResult = resposnse,
                    Total = resposnse.SearchResultItems.Count,
                    Success = resposnse?.SearchResultItems?.Count >= 0 ? true : false,
                    Message = "Success"

                };
            }
            catch (Exception ex)
            {
                model = new SearchResponseModel
                {
                    Message = $"Fail - {ex.Message}"
                };
            }
            return Ok(model);
        }
    }
}
