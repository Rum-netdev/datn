using EcommercialWebApp.Handler.Authentication.Commands;
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
        public async Task<IActionResult> Login([FromForm]LoginCommand request)
        {
            var result = await _broker.Command(request);

            if (result.IsLoggedIn)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register([FromForm]SignUpCommand request)
        {
            var result = await _broker.Command(request);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("/forgotpassword")]
        public async Task<IActionResult> ForgotPassword([FromForm] ForgotPasswordCommand request)
        {
            var result = _broker.Command(request);
            return Ok(result);
        }
    }
}
