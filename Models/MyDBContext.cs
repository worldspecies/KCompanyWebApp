using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace KCompanyWebApp.Models;

public partial class MyDBContext : DbContext
{
    public MyDBContext()
    {
    }

    public MyDBContext(DbContextOptions<MyDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MsCustomer> MsCustomers { get; set; }

    public virtual DbSet<MsMarketingArea> MsMarketingAreas { get; set; }

    public virtual DbSet<MsPage> MsPages { get; set; }

    public virtual DbSet<MsPricingConfig> MsPricingConfigs { get; set; }

    public virtual DbSet<ProductServiceModel> MsProductSpareparts { get; set; }

    public virtual DbSet<MsRoleAccess> MsRoleAccesses { get; set; }

    public virtual DbSet<MsStore> MsStores { get; set; }

    public virtual DbSet<TrOrderDtl> TrOrderDtls { get; set; }

    public virtual DbSet<TrOrderHdr> TrOrderHdrs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MsCustomer>(entity =>
        {
            entity.HasKey(e => e.CustomerNo);

            entity.ToTable("MsCustomer");

            entity.Property(e => e.CustomerNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ActiveFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CrtUsrId)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("CrtUsrID");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CustomerType)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ModUsrId)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("ModUsrID");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TsCrt).HasColumnType("datetime");
            entity.Property(e => e.TsMod).HasColumnType("datetime");
        });

        modelBuilder.Entity<MsMarketingArea>(entity =>
        {
            entity.HasKey(e => e.AreaNo);

            entity.ToTable("MsMarketingArea");

            entity.Property(e => e.AreaNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ActiveFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.AreaDesc)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CrtUsrId)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("CrtUsrID");
            entity.Property(e => e.ModUsrId)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("ModUsrID");
            entity.Property(e => e.TsCrt).HasColumnType("datetime");
            entity.Property(e => e.TsMod).HasColumnType("datetime");
        });

        modelBuilder.Entity<MsPage>(entity =>
        {
            entity.HasKey(e => e.PageNo);

            entity.ToTable("MsPage");

            entity.Property(e => e.PageNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ActiveFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PageController)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PageAction)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MsPricingConfig>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("MsPricingConfig");

            entity.Property(e => e.ActiveFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Amount)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(19, 2)");
            entity.Property(e => e.AreaNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CrtUsrId)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("CrtUsrID");
            entity.Property(e => e.CustomerType)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ModUsrId)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("ModUsrID");
            entity.Property(e => e.ProductNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StoreNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TsCrt).HasColumnType("datetime");
            entity.Property(e => e.TsMod).HasColumnType("datetime");
            entity.Property(e => e.ValidFrom).HasColumnType("date");
            entity.Property(e => e.ValidTo).HasColumnType("date");

            entity.HasOne(d => d.ProductNoNavigation).WithMany()
                .HasForeignKey(d => d.ProductNo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MsPricingConfig_MsProductSparepart");
        });

        modelBuilder.Entity<ProductServiceModel>(entity =>
        {
            entity.HasKey(e => e.ProductNo);

            entity.ToTable("MsProductSparepart");

            entity.Property(e => e.ProductNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ActiveFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Cogs)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(19, 2)")
                .HasColumnName("COGS");
            entity.Property(e => e.CrtUsrId)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("CrtUsrID");
            entity.Property(e => e.ModUsrId)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("ModUsrID");
            entity.Property(e => e.ProductBrand)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ProductDesc)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ProductType)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TsCrt).HasColumnType("datetime");
            entity.Property(e => e.TsMod).HasColumnType("datetime");
            entity.Property(e => e.UoM)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MsRoleAccess>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("MsRoleAccess");

            entity.Property(e => e.PageNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MsStore>(entity =>
        {
            entity.HasKey(e => e.StoreNo);

            entity.ToTable("MsStore");

            entity.Property(e => e.StoreNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ActiveFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.AreaNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CrtUsrId)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("CrtUsrID");
            entity.Property(e => e.ModUsrId)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("ModUsrID");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StoreDesc)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TsCrt).HasColumnType("datetime");
            entity.Property(e => e.TsMod).HasColumnType("datetime");

            entity.HasOne(d => d.AreaNoNavigation).WithMany(p => p.MsStores)
                .HasForeignKey(d => d.AreaNo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MsStore_MsMarketingArea");
        });

        modelBuilder.Entity<TrOrderDtl>(entity =>
        {
            entity.HasKey(e => e.OrderDtlID); 
            entity.ToTable("TrOrderDtl");
            entity.Property(e => e.OrderDtlID);

            entity.Property(e => e.ActiveFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.OrderNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Price)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(19, 2)");
            entity.Property(e => e.ProductNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Quantity).HasDefaultValueSql("((0))");
            entity.Property(e => e.Total)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(19, 2)");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.OrderNoNavigation).WithMany()
                .HasForeignKey(d => d.OrderNo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TrOrderDtl_TrOrderHdr");

            entity.HasOne(d => d.ProductNoNavigation).WithMany()
                .HasForeignKey(d => d.ProductNo)
                .HasConstraintName("FK_TrOrderDtl_MsProductSparepart");
        });

        modelBuilder.Entity<TrOrderHdr>(entity =>
        {
            entity.HasKey(e => e.OrderNo);

            entity.ToTable("TrOrderHdr");

            entity.Property(e => e.OrderNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ActiveFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.AreaNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CrtUsrId)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("CrtUsrID");
            entity.Property(e => e.CustomerNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.GrandTotal)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(19, 2)");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModUsrId)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("ModUsrID");
            entity.Property(e => e.OrderDate).HasColumnType("date");
            entity.Property(e => e.SalesmanNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StoreNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TsCrt).HasColumnType("datetime");
            entity.Property(e => e.TsMod).HasColumnType("datetime");

            entity.HasOne(d => d.CustomerNoNavigation).WithMany(p => p.TrOrderHdrs)
                .HasForeignKey(d => d.CustomerNo)
                .HasConstraintName("FK_TrOrderHdr_MsCustomer");

            entity.HasOne(d => d.StoreNoNavigation).WithMany(p => p.TrOrderHdrs)
                .HasForeignKey(d => d.StoreNo)
                .HasConstraintName("FK_TrOrderHdr_MsStore");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
