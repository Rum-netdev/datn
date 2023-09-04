using EcommercialWebApp.Data;
using EcommercialWebApp.Data.Models;
using EcommercialWebApp.Handler.Categories.Dtos;
using EcommercialWebApp.Handler.Infrastructure;

namespace EcommercialWebApp.Handler.Categories.Commands
{
    public class CreateCategoryCommand : CreateCategoryDto, ICommand<CreateCategoryCommandResult>
    {
    }

    public class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand, CreateCategoryCommandResult>
    {
        private readonly ApplicationDbContext _db;

        public CreateCategoryCommandHandler(
            ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<CreateCategoryCommandResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            CreateCategoryCommandResult result = new();

            if (string.IsNullOrEmpty(request.Name))
            {
                result.IsSuccess = false;
                result.Message = "The name must not be empty";

                return result;
            }

            Category category = new Category()
            {
                Name = request.Name,
                Description = request.Description
            };

            _db.Categories.Add(category);
            if (await _db.SaveChangesAsync() > 0)
            {
                result.IsSuccess = true;
                result.Message = "Creating category successfully";
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "An error may was happend when created category";
            }

            return result;

        }
    }

    public class CreateCategoryCommandResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public ICollection<string> Errors { get; set; }
    }
}
