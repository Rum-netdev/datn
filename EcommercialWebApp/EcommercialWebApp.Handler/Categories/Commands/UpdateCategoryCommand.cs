using EcommercialWebApp.Data;
using EcommercialWebApp.Handler.Categories.Dtos;
using EcommercialWebApp.Handler.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommercialWebApp.Handler.Categories.Commands
{
    public class UpdateCategoryCommand : UpdateCategoryDto, ICommand<UpdateCategoryCommandResult>
    {
    }

    public class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryCommand, UpdateCategoryCommandResult>
    {
        private readonly ApplicationDbContext _db;

        public UpdateCategoryCommandHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<UpdateCategoryCommandResult> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var result = new UpdateCategoryCommandResult();

            if (request.Id <= 0)
            {
                result.IsSuccess = false;
                result.Message = "Invalid category ID";

                return result;
            }
            if (string.IsNullOrEmpty(request.Name))
            {
                result.IsSuccess = false;
                result.Message = "The name of category must not be empty";

                return result;
            }

            var category = _db.Categories.Where(t => t.Id == request.Id)
                .FirstOrDefault();

            if (category == null)
            {
                result.IsSuccess = false;
                result.Message = "Category is not existing in system";

                return result;
            }

            category.Name = request.Name;
            category.Description = request.Description;

            int effectRows = await _db.SaveChangesAsync();
            result.IsSuccess = effectRows > 0;
            result.Message = effectRows > 0 ? "Update category successfully" : "Update category failed";

            return result;
        }
    }

    public class UpdateCategoryCommandResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
