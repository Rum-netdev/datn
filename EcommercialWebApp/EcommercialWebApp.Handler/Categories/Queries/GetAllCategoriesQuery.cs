using EcommercialWebApp.Data;
using EcommercialWebApp.Data.Models;
using EcommercialWebApp.Handler.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommercialWebApp.Handler.Categories.Queries
{
    public class GetAllCategoriesQuery : IQuery<GetAllCategoriesQueryResult>
    {

    }

    public class GetAllCategoriesQueryHandler : IQueryHandler<GetAllCategoriesQuery, GetAllCategoriesQueryResult>
    {
        private readonly ApplicationDbContext _db;

        public GetAllCategoriesQueryHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<GetAllCategoriesQueryResult> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var result = new GetAllCategoriesQueryResult();
            result.Categories = _db.Categories.ToList();
            result.IsSuccess = true;

            return result;
        }
    }

    public class GetAllCategoriesQueryResult
    {
        public ICollection<Category> Categories { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public ICollection<string> Errors { get; set; }
    }
}
