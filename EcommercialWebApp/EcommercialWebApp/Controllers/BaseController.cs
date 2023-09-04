using EcommercialWebApp.Core.Claims;
using Microsoft.AspNetCore.Mvc;

namespace EcommercialWebApp.Controllers
{
    //[ApiController]
    public class BaseController : ControllerBase
    {
        [HttpGet]
        private int GetCurrentUserId()
        {
            return int.Parse(HttpContext.User?.Claims
                .Where(t => t.Type == IdentityClaims.UserId)
                .FirstOrDefault().Value);
        }
    }
}
