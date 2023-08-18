using EcommercialWebApp.Data;
using EcommercialWebApp.Data.Models;
using EcommercialWebApp.Handler.Infrastructure;
using EcommercialWebApp.Handler.Products.Dtos;

namespace EcommercialWebApp.Handler.Products.Commands
{
    public class CreateProductCommand : CreateProductDto, ICommand<CreateProductCommandResult>
    {
    }

    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductCommandResult>
    {
        private readonly ApplicationDbContext _db;

        public CreateProductCommandHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<CreateProductCommandResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            CreateProductCommandResult result = new CreateProductCommandResult();

            if (string.IsNullOrEmpty(request.Name))
            {
                result.IsSuccess = false;
                result.Message = "The name must not be empty";

                return result;
            }

            if (request.Price < 0)
            {
                result.IsSuccess = false;
                result.Message = "The price must bigger than 0";

                return result;
            }

            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price
            };

            _db.Products.Add(product);
            if (await _db.SaveChangesAsync() > 0)
            {
                result.IsSuccess = true;
                result.Message = "Creating product succesfully";
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "An error has occured from server, please try again";
            }

            return result;
        }
    }

    public class CreateProductCommandResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
    }
}
