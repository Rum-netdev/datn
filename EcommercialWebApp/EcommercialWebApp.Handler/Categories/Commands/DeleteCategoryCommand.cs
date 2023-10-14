using EcommercialWebApp.Data;
using EcommercialWebApp.Handler.Infrastructure;

namespace EcommercialWebApp.Handler.Categories.Commands
{
    public class DeleteCategoryCommand : ICommand<DeleteCategoryCommandResult>
    {
        public int Id { get; set; }
    }

    public class DeleteCategoryCommandHandler : ICommandHandler<DeleteCategoryCommand, DeleteCategoryCommandResult>
    {
        private readonly ApplicationDbContext _db;

        public DeleteCategoryCommandHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<DeleteCategoryCommandResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var result = new DeleteCategoryCommandResult();

            if (request.Id <= 0)
            {
                result.IsSuccess = true;
                result.Message = "Invalid category ID";

                return result;
            }

            var category = _db.Categories.Where(t => t.Id == request.Id).FirstOrDefault();
            _db.Categories.Remove(category);

            int effectRows = await _db.SaveChangesAsync();

            result.IsSuccess = true;
            result.Message = "Remove category successfully";
            return result;
        }
    }

    public class DeleteCategoryCommandResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
