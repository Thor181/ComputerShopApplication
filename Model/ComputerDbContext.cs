using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ComputerShopApplication.Model
{
    public partial class ComputerDbContext : DbContext
    {
        public ComputerDbContext()
        {
        }

        public ComputerDbContext(DbContextOptions<ComputerDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Accessory> Accessories { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Manufacturer> Manufacturers { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(Service.Config.ConnectionString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Accessory>(entity =>
            {
                entity.HasKey(e => e.IdAccessory)
                    .HasName("PRIMARY");

                entity.ToTable("accessory");

                entity.HasIndex(e => e.IdCategory, "Accessory_Category_fkey_idx");

                entity.HasIndex(e => e.IdManufacturer, "Accessory_Manufacturer_fkey_idx");

                entity.Property(e => e.IdAccessory).HasColumnName("idAccessory");

                entity.Property(e => e.IdCategory).HasColumnName("idCategory");

                entity.Property(e => e.IdManufacturer).HasColumnName("idManufacturer");

                entity.Property(e => e.Image)
                    .HasColumnType("blob")
                    .HasColumnName("image");

                entity.Property(e => e.IsGaming).HasColumnName("isGaming");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasPrecision(18, 2)
                    .HasColumnName("price");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.Accessories)
                    .HasForeignKey(d => d.IdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("category");

                entity.HasOne(d => d.IdManufacturerNavigation)
                    .WithMany(p => p.Accessories)
                    .HasForeignKey(d => d.IdManufacturer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("manufacturer");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.IdCategory)
                    .HasName("PRIMARY");

                entity.ToTable("category");

                entity.Property(e => e.IdCategory).HasColumnName("idCategory");

                entity.Property(e => e.Name)
                    .HasMaxLength(75)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Manufacturer>(entity =>
            {
                entity.HasKey(e => e.IdManufacturer)
                    .HasName("PRIMARY");

                entity.ToTable("manufacturer");

                entity.Property(e => e.IdManufacturer).HasColumnName("idManufacturer");

                entity.Property(e => e.Name)
                    .HasMaxLength(75)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PRIMARY");

                entity.ToTable("user");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.Login)
                    .HasMaxLength(50)
                    .HasColumnName("login");

                entity.Property(e => e.Password)
                    .HasMaxLength(45)
                    .HasColumnName("password");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
