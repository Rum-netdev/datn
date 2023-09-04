using AutoMapper;
using AutoMapper.QueryableExtensions;
using EcommercialWebApp.Data;
using EcommercialWebApp.Handler.Infrastructure;
using EcommercialWebApp.Handler.Products.Dtos;
using Microsoft.EntityFrameworkCore;

namespace EcommercialWebApp.Handler.Products.Queries
{
    public class GetAllProductsQuery : IQuery<GetAllProductsQueryResult>
    {
    }

    public class GetAllProductsQueryHandler : IQueryHandler<GetAllProductsQuery, GetAllProductsQueryResult>
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(ApplicationDbContext db,
            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<GetAllProductsQueryResult> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            GetAllProductsQueryResult result = new();

            var products = await _db.Products
                .AsNoTracking()
                .OrderBy(t => t.CreatedAt)
                .ProjectTo<ProductViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            result.Products = products;

            return result;
        }
    }


    public class GetAllProductsQueryResult
    {
        public ICollection<ProductViewModel> Products { get; set; }
    }
}
