using EcommercialWebApp.Handler.Categories.Commands;
using EcommercialWebApp.Handler.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommercialWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CategoriesController : BaseController
    {
        private readonly IBroker _broker;

        public CategoriesController(IBroker broker)
        {
            _broker = broker;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            return Ok();
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateCategory(CreateCategoryCommand request)
        {
            var result = await _broker.Command(request);
            return Ok(result);
        }
    }
}
