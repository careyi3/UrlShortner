using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrlShortner.Services;

namespace UrlShortner.Controllers
{
    public class HomeController : Controller
    {
        private IUrlShortnerService _urlShortnerService;

        public HomeController(IUrlShortnerService urlShortnerService)
        {
            _urlShortnerService = urlShortnerService;
        }

        [Route("/")]
        [Route("/{code}")]
        [HttpGet]
        public async Task<IActionResult> Get(string code)
        {
            if(!String.IsNullOrEmpty(code))
            {
                var uri = await _urlShortnerService.FetchUrlFromCodeAsync(code);
                return RedirectPermanent(uri.AbsoluteUri);
            }
            return View("Create");
        }

        [Route("/")]
        [HttpPost]
        public async Task<IActionResult> Post(string url)
        {
            var uri = await _urlShortnerService.StoreUrlAsync(new Uri(url));
            ViewBag.Url = uri.AbsoluteUri;
            return View("View");
        }
    }
}
