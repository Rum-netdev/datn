using EcommercialWebApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommercialWebApp.Data.Configurations
{
    public class ProductsInCategoriesConfiguration : IEntityTypeConfiguration<ProductsInCategories>
    {
        public void Configure(EntityTypeBuilder<ProductsInCategories> builder)
        {
            builder.ToTable("ProductsInCategories");

            builder.HasKey(t => new { t.ProductId, t.CategoryId });

            builder.HasOne(s => s.Product)
                .WithMany(d => d.ProductsInCategories)
                .HasForeignKey(t => t.ProductId);

            builder.HasOne(s => s.Category)
                .WithMany(d => d.ProductsInCategories)
                .HasForeignKey(t => t.CategoryId);
        }
    }
}
