using Shop.Domain.Entities;
using Shop.Domain.Queries;

namespace Shop.Domain.Tests.Queries
{
    [TestClass]
    public class ProductQueriesTests
    {
        private IList<Product> _products;

        public ProductQueriesTests()
        {
            _products = new List<Product>();
            _products.Add(new Product("Produto 01", "this is a test", 10, true));
            _products.Add(new Product("Produto 02", "this is a test", 20, true));
            _products.Add(new Product("Produto 03", "this is a test", 30, true));
            _products.Add(new Product("Produto 04", "this is a test", 40, false));
            _products.Add(new Product("Produto 05", "this is a test", 50, false));
        }

        [TestMethod]
        [TestCategory("Queries")]
        public void Dado_a_consulta_de_produtos_ativos_deve_retornar_3()
        {
            var result = _products.AsQueryable().Where(ProductQueries.GetActiveProducts());
            Assert.AreEqual(result.Count(), 3);
        }

        [TestMethod]
        [TestCategory("Queries")]
        public void Dado_a_consulta_de_produtos_inativos_deve_retornar_2()
        {
            var result = _products.AsQueryable().Where(ProductQueries.GetInactiveProducts());
            Assert.AreEqual(result.Count(), 2);
        }
    }
}
