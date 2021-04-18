using Microsoft.EntityFrameworkCore;
using Product.API.Models;

namespace Product.API.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        {
        }

        public DbSet<Pie> Pies { get; set; }
        public DbSet<PieType> PieTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            SeedData(modelBuilder);

            modelBuilder.Entity<PieType>()
                .HasMany(p => p.Pies);

        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pie>().HasData(
              new Pie { Id = 1, Name = "Strawberry Pie", Price = 16.95, Description = "Icing carrot", TypeId = 2 },
              new Pie { Id = 2, Name = "Cheese cake", Price = 18.95, Description = "Jelly-o cheesecake", TypeId = 1 },
              new Pie { Id = 3, Name = "Rhubarb Pie", Price = 17.9, Description = "Sweet roll marzipan marshmallow", TypeId = 1 },
              new Pie { Id = 4, Name = "Pumpkin Pie", Price = 19.95, Description = "Chocolate cake gingerbread tootsie", TypeId = 2 },
              new Pie { Id = 5, Name = "Strawberry Pie", Price = 15.95, Description = "Icing carrot", TypeId = 3 },
              new Pie { Id = 6, Name = "Cheese cake", Price = 18.95, Description = "Jelly-o cheesecake", TypeId = 1 },
              new Pie { Id = 7, Name = "Rhubarb Pie", Price = 15.95, Description = "Sweet roll marzipan marshmallow", TypeId = 2 },
              new Pie { Id = 8, Name = "Pumpkin Pie", Price = 12.95, Description = "Chocolate cake gingerbread tootsie", TypeId = 2 }
            );

            modelBuilder.Entity<PieType>().HasData(
                new PieType { Id = 1, TypeName = "Fruit" },
                new PieType { Id = 2, TypeName = "Cheese" },
                new PieType { Id = 3, TypeName = "Cream" }
            );
        }


    }
}
