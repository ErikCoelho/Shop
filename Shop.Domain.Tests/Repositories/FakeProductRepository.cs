using Shop.Domain.Entities;
using Shop.Domain.Repositories;

namespace Shop.Domain.Tests.Repositories
{
    public class FakeProductRepository : IProductRepository
    {
        public IEnumerable<Product> Get(IEnumerable<Guid> ids)
        {
            IList<Product> products = new List<Product>();
            products.Add(new Product("Produto 1", "Celular", 10, true));
            products.Add(new Product("Produto 2", "Celular", 10, true));
            products.Add(new Product("Produto 3", "Celular", 10, true));
            products.Add(new Product("Produto 4", "Notebook", 10, false));
            products.Add(new Product("Produto 5", "Notebook", 10, false));

            return products;
        }
    }
}
