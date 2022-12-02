using Shop.Domain.Entities;
using System.Linq.Expressions;

namespace Shop.Domain.Queries
{
    public class CustomerQueries
    {
        public static Expression<Func<Customer, bool>> Get(string customer)
        {
            return x => x.Document.Number == customer;
        }
    }
}
