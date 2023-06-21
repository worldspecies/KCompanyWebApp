using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace KCompanyWebApp.Models;

public partial class MyDBAuth : DbContext
{
    public MyDBAuth()
    {
    }

    public MyDBAuth(DbContextOptions<MyDBAuth> options)
        : base(options)
    {
    }

    public virtual DbSet<MsUser> MsUsers { get; set; }

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
                .UseSqlServer(configuration.GetConnectionString("SSOConnection"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MsUser>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("MsUser");

            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UserID");
            entity.Property(e => e.ActiveFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.AreaNo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CrtUsrId)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("CrtUsrID");
            entity.Property(e => e.FullName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ModUsrId)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("ModUsrID");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.AccessToken)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TsCrt).HasColumnType("datetime");
            entity.Property(e => e.TsMod).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
