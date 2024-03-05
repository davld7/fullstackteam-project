using System;
using System.Collections.Generic;
using Ioon.Domain;
using Microsoft.EntityFrameworkCore;

namespace Ioon.Infrastructure;

public partial class KioscoDbContext : DbContext
{
    public KioscoDbContext()
    {
    }

    public KioscoDbContext(DbContextOptions<KioscoDbContext> options)
        : base(options)
    {
    }

  

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-JTDJ7I6;Initial Catalog=Kiosco.DB;Persist Security Info=True;User ID=sa;Password=123456;Encrypt=False;Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<Business>(entity =>
        {
            entity.HasKey(e => e.BusinessId).HasName("PK__Business__F1EAA36E6E18C7CB");

            entity.ToTable("Business", "BusinessData");

            entity.HasIndex(e => e.Email, "IX_Business_Email");

            entity.HasIndex(e => e.BusinessName, "IX_Business_Name");

            entity.HasIndex(e => e.Phone, "IX_Business_Phone");

            entity.HasIndex(e => e.Ruc, "IX_Business_RUC");

            entity.HasIndex(e => e.BusinessUuid, "IX_Business_UIID");

            entity.HasIndex(e => e.Email, "UQ_Business_Email").IsUnique();

            entity.HasIndex(e => e.Phone, "UQ_Business_Phone").IsUnique();

            entity.HasIndex(e => e.Ruc, "UQ_Business_RUC").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.BusinessName).HasMaxLength(50);
            entity.Property(e => e.BusinessUuid).HasColumnName("BusinessUUID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Phone).HasMaxLength(9);
            entity.Property(e => e.Ruc)
                .HasMaxLength(19)
                .HasColumnName("RUC");

            entity.HasOne(d => d.BusinessType).WithMany(p => p.Businesses)
                .HasForeignKey(d => d.BusinessTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Business_BusinessType");

            entity.HasOne(d => d.Department).WithMany(p => p.Businesses)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Business_Department");

            entity.HasOne(d => d.Owner).WithMany(p => p.Businesses)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Business_Owner");
        });

        modelBuilder.Entity<BusinessConfiguration>(entity =>
        {
            entity.HasKey(e => e.ConfigId).HasName("PK__Business__C3BC335CEA1E7767");

            entity.ToTable("BusinessConfigurations", "BusinessData");

            entity.Property(e => e.IsActiveIva).HasColumnName("IsActiveIVA");
            entity.Property(e => e.Iva)
                .HasDefaultValueSql("((0.00))")
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("IVA");
            entity.Property(e => e.ServiceCommerce).HasMaxLength(30);
            entity.Property(e => e.TokenCommerce).HasMaxLength(30);
            entity.Property(e => e.Usdexchange)
                .HasDefaultValueSql("((0.00))")
                .HasColumnType("decimal(5, 3)")
                .HasColumnName("USDExchange");

            entity.HasOne(d => d.Business).WithMany(p => p.BusinessConfigurations)
                .HasForeignKey(d => d.BusinessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BusinessConfigurations_Business");
        });

        modelBuilder.Entity<BusinessType>(entity =>
        {
            entity.HasKey(e => e.BusinessTypeId).HasName("PK__Business__1D43DEC062ECBE74");

            entity.ToTable("BusinessTypes", "SystemConfig");

            entity.Property(e => e.TypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A0B355EED4F");

            entity.ToTable("Categories", "BusinessData");

            entity.HasIndex(e => e.BusinessId, "IX_Categories_BusinessId");

            entity.HasIndex(e => e.CategoryUuid, "IX_Categories_CategoryUUID");

            entity.Property(e => e.CategoryName).HasMaxLength(50);
            entity.Property(e => e.CategoryUuid).HasColumnName("CategoryUUID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Business).WithMany(p => p.Categories)
                .HasForeignKey(d => d.BusinessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Categories_Business");
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.CurrencyId).HasName("PK__Currenci__14470AF085DD3DBD");

            entity.ToTable("Currencies", "SystemConfig");

            entity.Property(e => e.CurrencyCode).HasMaxLength(3);
            entity.Property(e => e.CurrencyName).HasMaxLength(20);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BED9119213D");

            entity.ToTable("Department", "SystemConfig");

            entity.Property(e => e.DepartmentName).HasMaxLength(50);
        });

        modelBuilder.Entity<LogsServer>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__LogsServ__5E5486481CCB99DF");

            entity.ToTable("LogsServer", "SystemConfig");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<OwnersBusiness>(entity =>
        {
            entity.HasKey(e => e.OwnerId).HasName("PK__OwnersBu__819385B895DBA1B1");

            entity.ToTable("OwnersBusiness", "UserManagement");

            entity.HasIndex(e => e.Email, "IX_Owners_Email");

            entity.HasIndex(e => e.Identification, "IX_Owners_Identification");

            entity.HasIndex(e => e.OwnerUuid, "IX_Owners_UUID");

            entity.HasIndex(e => e.Email, "UQ_Owners_Email").IsUnique();

            entity.HasIndex(e => e.Identification, "UQ_Owners_Identification").IsUnique();

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.Identification).HasMaxLength(16);
            entity.Property(e => e.LastActivityDate).HasColumnType("datetime");
            entity.Property(e => e.LastLoginDate).HasColumnType("datetime");
            entity.Property(e => e.LastPasswordChangedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.OwnerUuid).HasColumnName("OwnerUUID");
            entity.Property(e => e.PasswordHashed).HasMaxLength(32);
            entity.Property(e => e.PasswordSalt).HasMaxLength(32);
            entity.Property(e => e.Phone).HasMaxLength(9);
            entity.Property(e => e.RoleId).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Role).WithMany(p => p.OwnersBusinesses)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OwnersBusiness_Roles");
        });

        modelBuilder.Entity<PaymentType>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__PaymentT__9B556A38C947727C");

            entity.ToTable("PaymentTypes", "SystemConfig");

            entity.Property(e => e.PaymentName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6CD521D0B1D");

            entity.ToTable("Products", "BusinessData");

            entity.HasIndex(e => e.ProductUuid, "IX_Products_ProductUUID");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Discount)
                .HasDefaultValueSql("((0.00))")
                .HasColumnType("decimal(5, 2)");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ProductName).HasMaxLength(50);
            entity.Property(e => e.ProductUuid).HasColumnName("ProductUUID");

            entity.HasOne(d => d.Business).WithMany(p => p.Products)
                .HasForeignKey(d => d.BusinessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Business");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Categories");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A8B1D457A");

            entity.ToTable("Roles", "UserManagement");

            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionUuid).HasName("PK__Transact__21899632198332E8");

            entity.ToTable("Transactions", "BusinessData");

            entity.HasIndex(e => e.BusinessId, "IX_Transaction_Business");

            entity.HasIndex(e => e.Ntrasaction, "IX_Transaction_NTransaction");

            entity.HasIndex(e => e.PaymentTypeId, "IX_Transaction_PaymentType");

            entity.HasIndex(e => e.TransactionDate, "IX_Transaction_TransactionDate");

            entity.Property(e => e.TransactionUuid)
                .ValueGeneratedNever()
                .HasColumnName("TransactionUUID");
            entity.Property(e => e.AmountTotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Iva).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Ntrasaction)
                .HasMaxLength(50)
                .HasColumnName("NTrasaction");
            entity.Property(e => e.SubTotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TransactionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Business).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.BusinessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transaction_Business");

            entity.HasOne(d => d.Currency).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.CurrencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transaction_Currency");

            entity.HasOne(d => d.PaymentType).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.PaymentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transaction_PaymentType");
        });

        modelBuilder.Entity<TransactionDetail>(entity =>
        {
            entity.HasKey(e => e.TransactionDetailsId).HasName("PK__Transact__706E2B826E2DA9D2");

            entity.ToTable("TransactionDetails", "BusinessData");

            entity.Property(e => e.TransactionDetailsId).ValueGeneratedNever();
            entity.Property(e => e.Ivamount).HasColumnName("IVAmount");
            entity.Property(e => e.ProductName).HasMaxLength(50);
            entity.Property(e => e.TransactionUuid).HasColumnName("TransactionUUID");

            entity.HasOne(d => d.TransactionUu).WithMany(p => p.TransactionDetails)
                .HasForeignKey(d => d.TransactionUuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TransactionDetails_Transaction");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C8FA827F5");

            entity.ToTable("Users", "UserManagement");

            entity.HasIndex(e => e.Email, "IX_Users_Email");

            entity.HasIndex(e => e.Identification, "IX_Users_Identification");

            entity.HasIndex(e => e.UserUuid, "IX_Users_UserUUID");

            entity.HasIndex(e => e.Email, "UQ_Users_Email").IsUnique();

            entity.HasIndex(e => e.Identification, "UQ_Users_Identification").IsUnique();

            entity.Property(e => e.BusinessUuid).HasColumnName("BusinessUUID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.Identification).HasMaxLength(16);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PasswordHashed).HasMaxLength(32);
            entity.Property(e => e.PasswordSalt).HasMaxLength(32);
            entity.Property(e => e.UserUuid).HasColumnName("UserUUID");

            entity.HasOne(d => d.Business).WithMany(p => p.Users)
                .HasForeignKey(d => d.BusinessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Business");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
