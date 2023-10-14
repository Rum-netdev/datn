using AutoMapper;
using AutoMapper.QueryableExtensions;
using EcommercialWebApp.Data;
using EcommercialWebApp.Handler.Infrastructure;
using EcommercialWebApp.Handler.Products.Dtos;
using Microsoft.EntityFrameworkCore;

namespace EcommercialWebApp.Handler.Products.Queries
{
    public class GetProductByIdQuery : IQuery<GetProductByIdQueryResult>
    {
        public int Id { get; set; }
    }

    public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, GetProductByIdQueryResult>
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(ApplicationDbContext db,
            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<GetProductByIdQueryResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var result = new GetProductByIdQueryResult();

            var product = await _db.Products.Where(t => t.Id == request.Id)
                .ProjectTo<ProductViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (product != null)
            {
                result.Id = product.Id;
                result.Product = product;
                result.IsFound = true;
                result.IsSuccess = true;
                result.Message = "Found a product satisfy the condition";

                return result;
            }

            result.IsFound = false;
            result.IsSuccess = true;
            result.Message = "There's no product satisfy the condition";

            return result;
        }
    }


    public class GetProductByIdQueryResult
    {
        public int Id { get; set; }
        public ProductViewModel? Product { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsFound { get; set; }
        public string? Message { get; set; }
    }
}
