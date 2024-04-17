using ExceleTech.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExceleTech.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(o => o.UserId);
            builder.HasMany(o => o.LineItems)
                .WithOne()
                .HasForeignKey(li => li.OrderId);
            builder.Property(o => o.ExpectedDeliveryDate).IsRequired(false);
             builder.Property(o => o.ExpectedDeliveryDate).IsRequired(false);



        }
    }
}
