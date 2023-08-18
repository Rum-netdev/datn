using AutoMapper;
using EcommercialWebApp.Data;
using EcommercialWebApp.Handler.Infrastructure;
using EcommercialWebApp.Handler.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Task<GetAllProductsQueryResult> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }


    public class GetAllProductsQueryResult
    {
        public ICollection<ProductViewModel> Products { get; set; }
    }
}
