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
                optionsBuilder.UseSqlServer(Service.Config.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.Name)
                    .HasMaxLength(75)
                    .HasColumnName("Name");
            });

            modelBuilder.Entity<Manufacturer>(entity =>
            {
                entity.ToTable("Manufacturer");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.Name)
                    .HasMaxLength(75)
                    .HasColumnName("Name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.Login)
                    .HasMaxLength(50)
                    .HasColumnName("Login");

                entity.Property(e => e.Password)
                    .HasMaxLength(45)
                    .HasColumnName("Password");
            });

            modelBuilder.Entity<Accessory>(entity =>
            {
                entity.ToTable("Accessory");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.HasIndex(e => e.CategoryId, "Accessory_Category_fkey_idx");

                entity.HasIndex(e => e.ManufacturerId, "Accessory_Manufacturer_fkey_idx");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryId");

                entity.Property(e => e.ManufacturerId).HasColumnName("ManufacturerId");

                entity.Property(e => e.Image)
                    .HasColumnType("VARBINARY(MAX)")
                    .HasColumnName("Image");

                entity.Property(e => e.IsGaming).HasColumnName("IsGaming");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("Name");

                entity.Property(e => e.Price)
                    .HasPrecision(18, 2)
                    .HasColumnName("Price");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.Accessories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category");

                entity.HasOne(d => d.IdManufacturerNavigation)
                    .WithMany(p => p.Accessories)
                    .HasForeignKey(d => d.ManufacturerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Manufacturer");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
