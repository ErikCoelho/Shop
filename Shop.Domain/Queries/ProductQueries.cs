using Shop.Domain.Entities;
using System.Linq.Expressions;

namespace Shop.Domain.Queries
{
    public static class ProductQueries
    {
        public static Expression<Func<Product, bool>> GetActiveProducts()
        {
            return x => x.Active;
        }

        public static Expression<Func<Product, bool>> GetInactiveProducts()
        {
            return x => x.Active == false;
        }
    }
}
