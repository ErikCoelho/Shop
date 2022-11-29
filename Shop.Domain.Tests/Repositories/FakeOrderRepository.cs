using Shop.Domain.Entities;
using Shop.Domain.Repositories;

namespace Shop.Domain.Tests.Repositories
{
    public class FakeOrderRepository : IOrderRepository
    {
        public IEnumerable<Order> GetAll(string customer)
        {
            throw new NotImplementedException();
        }

        public void Save(Order order)
        {
        }
    }
}
