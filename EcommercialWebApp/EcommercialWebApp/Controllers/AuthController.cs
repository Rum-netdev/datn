using EcommercialWebApp.Handler.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EcommercialWebApp.Controllers
{
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IBroker _broker;

        public AuthController(IBroker broker)
        {
            _broker = broker;
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login()
        {

        }
    }
}
