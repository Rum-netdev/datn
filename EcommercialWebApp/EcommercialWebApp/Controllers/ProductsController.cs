using EcommercialWebApp.Handler.Infrastructure;
using EcommercialWebApp.Handler.Products.Commands;
using EcommercialWebApp.Handler.Products.Queries;
using Microsoft.AspNetCore.Mvc;

namespace EcommercialWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductsController : BaseController
    {
        private readonly IBroker _broker;

        public ProductsController(IBroker broker)
        {
            _broker = broker;
        }

        [HttpGet]
        public async Task<GetAllProductsQueryResult> GetAllProducts()
        {
            return await _broker.Query(new GetAllProductsQuery());
        }

        [HttpGet]
        public async Task<GetProductsPagingQueryResult> GetProductsPaging([FromQuery]GetProductsPagingQuery request)
        {
            var products = await _broker.Query(request);
            return products;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommand request)
        {
            var result = await _broker.Command(request);
            
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
