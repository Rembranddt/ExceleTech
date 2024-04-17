using ExceleTech.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExceleTech.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Email).HasMaxLength(100);
            builder.Property(u => u.Name).HasMaxLength(100);
            builder.Property(u => u.Password).HasMaxLength(250);
            builder.Property(u => u.BasketId).IsRequired(false);



            builder.OwnsOne(u => u.UserToken, Tokenbuilder =>
            Tokenbuilder.Property(t => t.RefreshToken).IsRequired());
            builder.Navigation(u => u.UserToken).IsRequired(false);



        }
    }
}
