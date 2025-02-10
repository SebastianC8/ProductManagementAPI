using Microsoft.EntityFrameworkCore;
using Repository.Contracts;
using Repository.Data.Entities;

namespace Repository.Implementation
{
    public class ProductRepositoryImpl : IProductRepository
    {

        private StoreContext _storeContext;

        // Dependency injection
        public ProductRepositoryImpl(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task AddAsync(Product product)
        {
            await _storeContext.Products.AddAsync(product);
            await _storeContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Product? product = await _storeContext.Products.FindAsync(id);

            if (product != null)
            {
                _storeContext.Products.Remove(product);
                await _storeContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _storeContext.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _storeContext.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateAsync(Product product)
        {
            _storeContext.Products.Attach(product);
            _storeContext.Products.Entry(product).State = EntityState.Modified;
            await _storeContext.SaveChangesAsync();
        }
    }
}
