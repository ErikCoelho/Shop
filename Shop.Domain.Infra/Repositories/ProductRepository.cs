using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Domain.Infra.Contexts;
using Shop.Domain.Queries;
using Shop.Domain.Repositories;

namespace Shop.Domain.Infra.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        public Product GetById(Guid id)
        {
            return _context.Products.FirstOrDefault(x => x.Id == id)!;
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
    }
}
