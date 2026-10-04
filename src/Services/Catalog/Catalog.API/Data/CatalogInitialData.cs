using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();
            if(await session.Query<Product>().AnyAsync(token: cancellation))
            {
                return;
            }

            session.Store<Product>(GetPreConfiguredProducts());
            await session.SaveChangesAsync(cancellation);
        }

        private static IEnumerable<Product> GetPreConfiguredProducts() => new List<Product>
        {
            new Product()
            {
                Id = new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"),
                Name = "RealMe p4 Pro",
                Description = "All Round Performance with smooth display and long battery life.",
                ImageFile = "product-1.png",
                Price = 20000.00M,
                Categories = new List<string> { "Smart Phone", "Realme" }
            },
            new Product()
            {
                Id = new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914"),
                Name = "Samsung Galaxy S24",
                Description = "Flagship camera capabilities with advanced AI features.",
                ImageFile = "product-2.png",
                Price = 79999.00M,
                Categories = new List<string> { "Smart Phone", "Samsung" }
            },
            new Product()
            {
                Id = new Guid("4f13a032-4cb8-4c96-9f64-b927384730a3"),
                Name = "iPhone 15 Pro",
                Description = "Titanium design with A17 Pro chip and customizable Action button.",
                ImageFile = "product-3.png",
                Price = 134900.00M,
                Categories = new List<string> { "Smart Phone", "Apple" }
            },
            new Product()
            {
                Id = new Guid("b482b8b9-e168-4504-89f5-4700f124c619"),
                Name = "Xiaomi 14",
                Description = "Leica professional optics with Snapdragon 8 Gen 3 processor.",
                ImageFile = "product-4.png",
                Price = 69999.00M,
                Categories = new List<string> { "Smart Phone", "Xiaomi" }
            },
            new Product()
            {
                Id = new Guid("868d447d-089c-462f-98f5-9372e3a1f185"),
                Name = "OnePlus 12",
                Description = "Smooth performance with 100W SuperVOOC fast charging.",
                ImageFile = "product-5.png",
                Price = 64999.00M,
                Categories = new List<string> { "Smart Phone", "OnePlus" }
            },
            new Product()
            {
                Id = new Guid("962649b1-5d2d-458f-b969-92c10b7f872d"),
                Name = "Google Pixel 8",
                Description = "Best-in-class computational photography with Google Tensor G3.",
                ImageFile = "product-6.png",
                Price = 75999.00M,
                Categories = new List<string> { "Smart Phone", "Google" }
            },
            new Product()
            {
                Id = new Guid("14d682df-64d6-41f3-808d-b089c2fa74db"),
                Name = "Nothing Phone (2)",
                Description = "Unique Glyph interface design with clean Nothing OS experience.",
                ImageFile = "product-7.png",
                Price = 37999.00M,
                Categories = new List<string> { "Smart Phone", "Nothing" }
            }
        };
    }
}
