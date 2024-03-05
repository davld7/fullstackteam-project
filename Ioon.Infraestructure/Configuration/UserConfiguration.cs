using Ioon.Domain;
using Ioon.Domain.Primitives.Entities;
using Ioon.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ioon.Infrastructure.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "UserManagement");

            builder.HasNoKey();

            builder.Property(x => x.UserId).HasConversion(
                userId => userId.Value,
                value => new UserId(value))
                .HasColumnName("UserUUID")
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            builder.Property(x => x.BusinessId)
                .HasColumnName("BusinessUUID")
                .HasConversion(
                    businessId => Guid.Parse(businessId),
                    guid => guid.ToString())
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            builder.Property(x => x.UserName).HasConversion(
                 userName => userName.Value,
                 value => UserName.Create(value)!)
                .HasColumnName("FullName")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Email).HasConversion(
                email => email.Value,
                value => EmailAddress.Create(value)!)
                .HasColumnName("Email")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.PasswordHashed)
                .HasColumnName("PasswordHashed")
                .HasColumnType("VARBINARY")
                .HasMaxLength(32)
                .IsRequired();

            builder.Property(x => x.PasswordSalt)
                .HasColumnName("PasswordSalt")
                .HasColumnType("VARBINARY")
                .HasMaxLength(32)
                .IsRequired();

            builder.Property(x => x.Identification).HasConversion(
                identifier => identifier.Value,
                value => Identification.Create(value)!)
                .HasColumnName("Identification")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(16)
                .IsRequired();

            builder.Property(x => x.RoleId)
                .HasColumnName("RoleId")
                .HasConversion(
                    roleId => Guid.Parse(roleId),
                    guid => guid.ToString())
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();
        }

    }
}
