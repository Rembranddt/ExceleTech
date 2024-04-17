using ExceleTech.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExceleTech.Infrastructure.Configurations
{
    public class BasketLineItemConfiguration : IEntityTypeConfiguration<BasketLineItem>
    {
        public void Configure(EntityTypeBuilder<BasketLineItem> builder)
        {


            builder.HasIndex(li => li.Id);
            builder.Property(li => li.Id).ValueGeneratedNever();
            builder.HasOne<Basket>()    
                .WithMany()
                .HasForeignKey(li => li.BasketId);
           builder.HasOne(li => li.Product)
                .WithMany()
                .HasForeignKey(li => li.ProductId);
           
        }
    }
}
