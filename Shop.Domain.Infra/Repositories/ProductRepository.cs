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

        public Product GetById(Guid id)
        {
            var productCache =  _cache.Get(id.ToString());
            Product product;

            if (!string.IsNullOrWhiteSpace(productCache))
            {
                product = JsonConvert.DeserializeObject<Product>(productCache);
                return product;
            }

            product = _context.Products.FirstOrDefault(x => x.Id == id);
            _cache.Set(id.ToString(), JsonConvert.SerializeObject(product));

            return product;
        }

        public IEnumerable<Product> GetActiveProducts()
        {
            return _context.Products.AsNoTracking().Where(ProductQueries.GetActiveProducts());
        }

        public IEnumerable<Product> GetInactiveProducts()
        {
            return _context.Products.AsNoTracking().Where(ProductQueries.GetInactiveProducts());
        }

        public IEnumerable<Product> Get(IEnumerable<Guid> ids)
        {
            var products = new List<Product>();
            foreach(var id in ids)
            {
                products.Add(GetById(id));
            }
            return products;
        }

        public void Create(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public void Delete(Product product)
        {
            _context.Products.Remove(product); 
            _context.SaveChanges();
        }

    }
}
