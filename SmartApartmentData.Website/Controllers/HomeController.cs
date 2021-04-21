using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmartApartmentData.Framework.Models;
using SmartApartmentData.Website.Extensions;
using SmartApartmentData.Website.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SmartApartmentData.Website.Controllers
{
    public class HomeController : Controller
    {
        HttpClient httpClient = new HttpClient();

        public HomeController()
        {
            httpClient.BaseAddress = new Uri("http://localhost:58158/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/text"));
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Search(string query, string markets)
        {
            var response = httpClient.PostAsJsonAsync("Search", new
            {
                Query = query,
                Markets = markets,
                Limit = 5
            });
            var responseContent = response.Result.Content.ReadAsStringAsync().Result;
            var searchResult = JsonConvert.DeserializeObject<SearchResponseModel>(responseContent);
            var model = searchResult.ToSearchResultItemViewModel();

            return Ok(model);
        }
    }
}
