using ExceleTech.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExceleTech.Infrastructure.Configurations
{
    public class OrderLineItemConfiguration : IEntityTypeConfiguration<OrderLineItem>
    {
        public void Configure(EntityTypeBuilder<OrderLineItem> builder)
        {
            builder.HasKey(li => li.Id);
            builder.OwnsOne(li => li.ProductPrice);
            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(li => li.ProductId);

        }
    }
}
