using System;
using BestBeforeAzure.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace BestBeforeAzure.Infrastructure.SharedKernel
{
    public class BestBeforeDbContext : DbContext
    {
        public BestBeforeDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // the container name
            modelBuilder.HasDefaultContainer("Products");
            
            // ProfileMaster has many educations and Many Experiences
            //modelBuilder.Entity().OwnsMany(e => e.Educations);
            //modelBuilder.Entity().OwnsMany(e => e.Experience);
        }
    }
}
