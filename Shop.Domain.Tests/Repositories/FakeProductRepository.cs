using Shop.Domain.Entities;
using Shop.Domain.Repositories;

namespace Shop.Domain.Tests.Repositories
{
    public class FakeProductRepository : IProductRepository
    {
        public void Create(Product product)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product product)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> Get(IEnumerable<Guid> ids)
        {
            IList<Product> products = new List<Product>();
            products.Add(new Product(null, "Produto 1", "Celular", 10, "true"));
            products.Add(new Product(null, "Produto 2", "Celular", 10, "true"));
            products.Add(new Product(null, "Produto 3", "Celular", 10, "true"));
            products.Add(new Product(null, "Produto 4", "Notebook", 10, "false"));
            products.Add(new Product(null, "Produto 5", "Notebook", 10, "false"));

            return products;
        }

        public IEnumerable<Product> GetActiveProducts()
        {
            throw new NotImplementedException();
        }

        public Product GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetInactiveProducts()
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
