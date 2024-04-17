using ExceleTech.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ExceleTech.Infrastructure.Configurations
{
    public class BasketConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder.HasIndex(b=>b.Id);
            builder.HasMany(b => b.LineItems)
                .WithOne()
                .HasForeignKey(li => li.BasketId);

            builder.HasOne<User>().WithOne().HasForeignKey<User>(b => b.BasketId);


        }
    }
}
