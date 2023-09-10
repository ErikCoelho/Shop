using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shop.Domain.Entities;
using Shop.Domain.Infra.Caching;
using Shop.Domain.Infra.Contexts;
using Shop.Domain.Queries;
using Shop.Domain.Repositories;

namespace Shop.Domain.Infra.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;
        private readonly ICachingService _cache;

        public ProductRepository(ICachingService cache, DataContext context)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            var productCache = await _cache.GetAsync(id.ToString());
            Product product;

            if (!string.IsNullOrWhiteSpace(productCache))
            {
                product = JsonConvert.DeserializeObject<Product>(productCache);
            }
            else
            {
                product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
                await _cache.SetAsync(id.ToString(), JsonConvert.SerializeObject(product));
            }

            return product;
        }

        public async Task<IEnumerable<Product>> GetActiveProductsAsync(int page, int pageSize)
        {
            string cacheKey = $"ActiveProductsPage_{page}_{pageSize}";

            var productCache = await _cache.GetAsync(cacheKey);
            IEnumerable<Product> activeProducts;

            if (!string.IsNullOrWhiteSpace(productCache))
            {
                activeProducts = JsonConvert.DeserializeObject<IEnumerable<Product>>(productCache);
            }
            else
            {
                activeProducts = await _context.Products
                    .AsNoTracking()
                    .Where(ProductQueries.GetActiveProducts())
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .OrderByDescending(x => x.LastUpdateDate)
                    .ToListAsync();

                await _cache.SetAsync(cacheKey, JsonConvert.SerializeObject(activeProducts));
            }

            return activeProducts;
        }

        public async Task<IEnumerable<Product>> GetInactiveProductsAsync()
        {
            return await _context.Products.AsNoTracking().Where(ProductQueries.GetInactiveProducts()).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAsync(IEnumerable<Guid> ids)
        {
            var tasks = ids.Select(id => GetByIdAsync(id));
            var products = await Task.WhenAll(tasks);
            return products;
        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(string term)
        {
            var searchResults = await _context.Products
                .AsNoTracking()
                .Where(p => p.Title.Contains(term) || p.Description.Contains(term))
                .ToListAsync();

            return searchResults;
        }

        public async Task CreateAsync(Product product)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
