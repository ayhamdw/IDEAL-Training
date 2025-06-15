using System;
using System.Collections.Generic;
using DataEntity.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace DataEntity.Models;

public partial class ProjectBaseContext : DbContext
{
    public ProjectBaseContext()
    {
    }

    public ProjectBaseContext(DbContextOptions<ProjectBaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<DataBaseScript> DataBaseScripts { get; set; }

    public virtual DbSet<DetailsLookup> DetailsLookups { get; set; }

    public virtual DbSet<DetailsLookupTranslation> DetailsLookupTranslations { get; set; }

    public virtual DbSet<EfmigrationsHistory> EfmigrationsHistories { get; set; }

    public virtual DbSet<MasterLookup> MasterLookups { get; set; }

    public virtual DbSet<MasterLookupTranslation> MasterLookupTranslations { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    public virtual DbSet<ScriptStatus> ScriptStatuses { get; set; }

    public virtual DbSet<SystemLog> SystemLogs { get; set; }

    public virtual DbSet<SystemSetting> SystemSettings { get; set; }

    public virtual DbSet<SystemSettingTranslation> SystemSettingTranslations { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }
    
    public virtual DbSet<Brand> Brands { get; set; }
    
    public virtual DbSet<Category> Category { get; set; }
    public virtual DbSet<Advertisement> Advertisements { get; set; }

    public virtual DbSet<Product> Product { get; set; }
    
    public virtual DbSet<ProductCategory> ProductCategory { get; set; }

    public DbSet<Promotion> Promotions { get; set; }



    //     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //         => optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=Root@123;database=ProjectBase", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.36-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasMaxLength(450);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.RoleId).HasMaxLength(450);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasMaxLength(450);
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.LockoutEnd).HasMaxLength(6);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.UserId).HasMaxLength(450);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);
            entity.Property(e => e.UserId).HasMaxLength(450);
        });

        modelBuilder.Entity<AspNetUserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.RoleId).HasMaxLength(450);
            entity.Property(e => e.UserId).HasMaxLength(450);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

            entity.Property(e => e.UserId).HasMaxLength(450);
            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);
        });

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Action).HasMaxLength(200);
            entity.Property(e => e.Controller).HasMaxLength(200);
            entity.Property(e => e.CreatedBy).HasMaxLength(256);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime(3)");
            entity.Property(e => e.IpAddress).HasMaxLength(200);
        });

        modelBuilder.Entity<DataBaseScript>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime(3)");
        });

        modelBuilder.Entity<DetailsLookup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("DetailsLookup");

            entity.Property(e => e.Code).HasMaxLength(300);
            entity.Property(e => e.CreatedBy).HasMaxLength(256);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime(3)");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime(3)");
            entity.Property(e => e.Value).HasMaxLength(200);
        });

        modelBuilder.Entity<DetailsLookupTranslation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("DetailsLookupTranslation");

            entity.Property(e => e.Value).HasMaxLength(200);
        });

        modelBuilder.Entity<EfmigrationsHistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");

            entity.ToTable("__EFMigrationsHistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<MasterLookup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("MasterLookup");

            entity.Property(e => e.Code).HasMaxLength(200);
            entity.Property(e => e.CreatedBy).HasMaxLength(256);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime(3)");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime(3)");
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<MasterLookupTranslation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("MasterLookupTranslation");

            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Permission");

            entity.Property(e => e.CreatedBy).HasMaxLength(256);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime(3)");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime(3)");
            entity.Property(e => e.PageName).HasMaxLength(300);
            entity.Property(e => e.PermissionKey).HasMaxLength(200);
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.RoleId).HasMaxLength(450);
        });

        modelBuilder.Entity<ScriptStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ScriptStatus");
        });

        modelBuilder.Entity<SystemLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("SystemLog");

            entity.Property(e => e.Component).HasMaxLength(200);
            entity.Property(e => e.CreatedBy).HasMaxLength(256);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime(3)");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime(3)");
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<SystemSetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("SystemSetting");

            entity.Property(e => e.CreatedBy).HasMaxLength(256);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime(3)");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime(3)");
        });

        modelBuilder.Entity<SystemSettingTranslation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("SystemSettingTranslation");
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("UserProfile");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime(3)");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime(3)");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.LastLogin).HasColumnType("datetime(3)");
            entity.Property(e => e.Username).HasMaxLength(256);
            entity.Property(e => e.FirstName).HasMaxLength(256);
            entity.Property(e => e.LastName).HasMaxLength(256);
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
            entity.ToTable("Brand");
            entity.Property(e => e.CreatedBy).HasMaxLength(256);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime(3)");
            entity.Property(e => e.Logo).HasMaxLength(500);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
            entity.ToTable("Category");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime(3)");
            entity.Property(e => e.UpdatedAt).HasMaxLength(256);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.Image).HasMaxLength(500);
            entity.Property(e => e.ParentId).HasMaxLength(256);
            entity.Property(e => e.Status).HasMaxLength(256);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
            entity.ToTable("Product");
            entity.Property(e => e.CreatedBy).HasMaxLength(256);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.BrandId).HasMaxLength(256);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime(3)");
            entity.Property(e => e.Status).HasMaxLength(256);
            entity.Property(e => e.SalePercentage).HasMaxLength(256);
            entity.Property(e => e.OriginalPrice).HasMaxLength(256);
            entity.Property(e => e.Image).HasMaxLength(1000);
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.CategoryId }).HasName("PRIMARY");
            entity.ToTable("ProductCategory");
            entity.Property(e => e.ProductId).HasMaxLength(256);
            entity.Property(e => e.CategoryId).HasMaxLength(256);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
