using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using AppAnimals.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AppAnimals.DAL
{
    public partial class AnimalsStoreContext : DbContext
    {
        public IConfiguration Configuration { get; }

        public AnimalsStoreContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public AnimalsStoreContext()
        {
        }

        public AnimalsStoreContext(DbContextOptions<AnimalsStoreContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }

        
        public virtual DbSet<AnimalLocation> AnimalLocation { get; set; }
        public virtual DbSet<AnimalRegion> AnimalRegion { get; set; }
        public virtual DbSet<AnimalSkinColor> AnimalSkinColor { get; set; }
        public virtual DbSet<AnimalType> AnimalType { get; set; }
        public virtual DbSet<Animals> Animals { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var connString = Configuration.GetConnectionString("DbConnection");
            optionsBuilder.UseSqlServer(connString);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<AnimalLocation>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<AnimalRegion>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<AnimalSkinColor>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(30);
            });

            modelBuilder.Entity<AnimalType>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Animals>(entity =>
            {
                entity.Property(e => e.AnimalLocationId).HasColumnName("animal_location_id");

                entity.Property(e => e.AnimalRegionId).HasColumnName("animal_region_id");

                entity.Property(e => e.AnimalSkinColorId).HasColumnName("animal_skin_color_id");

                entity.Property(e => e.AnimalTypeId).HasColumnName("animal_type_id");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.AnimalLocation)
                    .WithMany(p => p.Animals)
                    .HasForeignKey(d => d.AnimalLocationId)
                    .HasConstraintName("FK_Animals_AnimalLocation");

                entity.HasOne(d => d.AnimalRegion)
                    .WithMany(p => p.Animals)
                    .HasForeignKey(d => d.AnimalRegionId)
                    .HasConstraintName("FK_Animals_AnimalRegion");

                entity.HasOne(d => d.AnimalSkinColor)
                    .WithMany(p => p.Animals)
                    .HasForeignKey(d => d.AnimalSkinColorId)
                    .HasConstraintName("FK_Animals_AnimalSkinColor");

                entity.HasOne(d => d.AnimalType)
                    .WithMany(p => p.Animals)
                    .HasForeignKey(d => d.AnimalTypeId)
                    .HasConstraintName("FK_Animals_AnimalType");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });
        }
    }
}
