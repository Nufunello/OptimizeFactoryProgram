using Microsoft.EntityFrameworkCore;

namespace OptimizeFactoryProgram.Context
{
    public class DBContext : DbContext
    {
        public DbSet<Material> Materials { get; set; }
        public DbSet<Price<Material>> PricesMaterial { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Price<Product>> PricesProduct { get; set; }
        public DbSet<MaterialAvailable> MaterialAvailable { get; set; }
        public DBContext()
        {
            this.Database.EnsureCreated();
        }
        public DBContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("AppDb");
            optionsBuilder.UseSqlServer(connectionString);
        }
        public void Seed()
        {
            var context = this;
            context.Materials.Add(new Material { Measure = Measures.apiece, Name = "Дерево" });
            context.Materials.Add(new Material { Measure = Measures.kilogram, Name = "Залізо" });
            context.Materials.Add(new Material { Measure = Measures.kilogram, Name = "Золото" });
            context.Materials.Add(new Material { Measure = Measures.apiece, Name = "Папір" });
            context.SaveChanges();

            context.MaterialAvailable.Add(new MaterialAvailable { Material = context.Materials.Where(x => x.Name == "Дерево").First(), Count = 100 });
            context.MaterialAvailable.Add(new MaterialAvailable { Material = context.Materials.Where(x => x.Name == "Залізо").First(), Count = 0.35 });
            context.MaterialAvailable.Add(new MaterialAvailable { Material = context.Materials.Where(x => x.Name == "Золото").First(), Count = 0.15 });
            context.MaterialAvailable.Add(new MaterialAvailable { Material = context.Materials.Where(x => x.Name == "Папір").First(), Count = 500 });
            context.SaveChanges();

            context.PricesMaterial.Add(new Price<Material> { PriceOf = context.Materials.Where(x => x.Name == "Дерево").First(), Count = 0.01, Cost = 3 });
            context.PricesMaterial.Add(new Price<Material> { PriceOf = context.Materials.Where(x => x.Name == "Залізо").First(), Count = 0.01, Cost = 4 });
            context.PricesMaterial.Add(new Price<Material> { PriceOf = context.Materials.Where(x => x.Name == "Золото").First(), Count = 1, Cost = 9 });
            context.PricesMaterial.Add(new Price<Material> { PriceOf = context.Materials.Where(x => x.Name == "Папір").First(), Count = 1, Cost = 1 });
            context.SaveChanges();

            context.Products.Add
            (
                new Product
                {
                    Name = "Молоток",
                    Measure = Measures.apiece,
                    Receipt = new List<Product.Ingredient>
                    {
                        new Product.Ingredient { Material = context.Materials.Where(x => x.Name == "Залізо").First(), Count = 0.02 },
                        new Product.Ingredient { Material = context.Materials.Where(x => x.Name == "Дерево").First(), Count = 4 }
                    }
                }
            );
            context.Products.Add
            (
                new Product
                {
                    Name = "Золоте кільце",
                    Measure = Measures.apiece,
                    Receipt = new List<Product.Ingredient>
                    {
                        new Product.Ingredient { Material = context.Materials.Where(x => x.Name == "Залізо").First(), Count = 0.02 },
                        new Product.Ingredient { Material = context.Materials.Where(x => x.Name == "Золото").First(), Count = 0.03 }
                    }
                }
            );
            context.Products.Add
            (
                new Product
                {
                    Name = "Зошит",
                    Measure = Measures.apiece,
                    Receipt = new List<Product.Ingredient>
                    {
                        new Product.Ingredient { Material = context.Materials.Where(x => x.Name == "Папір").First(), Count = 60 },
                    }
                }
            );
            context.SaveChanges();

            context.PricesProduct.Add
            (
                new Price<Product> { PriceOf = context.Products.Where(x => x.Name == "Молоток").First(), Count = 1, Cost = 30 }
            );
            context.PricesProduct.Add
            (
                new Price<Product> { PriceOf = context.Products.Where(x => x.Name == "Золоте кільце").First(), Count = 1, Cost = 100 }
            );
            context.PricesProduct.Add
            (
                new Price<Product> { PriceOf = context.Products.Where(x => x.Name == "Зошит").First(), Count = 1, Cost = 5 }
            );
            context.SaveChanges();
        }
    }
}
