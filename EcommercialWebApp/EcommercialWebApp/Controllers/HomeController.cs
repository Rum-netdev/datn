using EcommercialWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EcommercialWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }
        
        [HttpGet]
        public IActionResult Privacy()
        {
            return Ok();
        }

        [HttpGet]
        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            //return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            return Ok();
        }
    }
}