namespace EcommercialWebApp.Handler.Products.Dtos
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<int> Categories { get; set; }
    }
}
