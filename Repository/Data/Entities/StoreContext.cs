using Microsoft.EntityFrameworkCore;

namespace Repository.Data.Entities
{
    public class StoreContext: DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options) {}

        public DbSet<Product> Products { get; set; }

    }
}
