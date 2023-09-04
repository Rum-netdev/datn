using AutoMapper;
using AutoMapper.QueryableExtensions;
using EcommercialWebApp.Data;
using EcommercialWebApp.Handler.Commons;
using EcommercialWebApp.Handler.Infrastructure;
using EcommercialWebApp.Handler.Products.Dtos;
using Microsoft.EntityFrameworkCore;

namespace EcommercialWebApp.Handler.Products.Queries
{
    public class GetProductsPagingQuery : IQuery<GetProductsPagingQueryResult>
    {
        public string SearchData { get; set; } = string.Empty;
        public decimal? FromPrice { get; set; }
        public decimal? ToPrice { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }

    public class GetProducstPagingQueryHandler : IQueryHandler<GetProductsPagingQuery, GetProductsPagingQueryResult>
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public GetProducstPagingQueryHandler(
            ApplicationDbContext db,
            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<GetProductsPagingQueryResult> Handle(GetProductsPagingQuery request, CancellationToken cancellationToken)
        {
            GetProductsPagingQueryResult result = new();
            var query = _db.Products.AsQueryable();
            
            if (!string.IsNullOrEmpty(request.SearchData))
            {
                query = query.Where(t => t.Name.Contains(request.SearchData));
            }

            if (request.FromPrice.HasValue)
            {
                query = query.Where(t => t.Price >= request.FromPrice.Value);
            }

            if (request.ToPrice.HasValue)
            {
                query = query.Where(t => t.Price < request.ToPrice.Value);
            }

            var products = await query
                .AsNoTracking()
                .OrderBy(t => t.CreatedAt)
                .Skip((1 - request.PageNumber) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<ProductViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            result.Products = new PaginatedList<ProductViewModel>() { Data = products };
            result.Products.TotalPage = Convert.ToInt32(products.Count / request.PageSize);

            return result;
        }
    }

    public class GetProductsPagingQueryResult 
    {
        public PaginatedList<ProductViewModel> Products { get; set; }
    }
}
