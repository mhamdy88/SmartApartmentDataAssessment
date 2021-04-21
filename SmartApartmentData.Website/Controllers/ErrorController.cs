using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartApartmentData.Website.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet]
        [Route("/Error")]
        public IActionResult Error()
        {
            var ex = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.ExceptionName = ex.Error.GetType().ToString();
            ViewBag.ExceptionMessage = ex.Error.Message;
            return View("Error");
        }
    }
}
