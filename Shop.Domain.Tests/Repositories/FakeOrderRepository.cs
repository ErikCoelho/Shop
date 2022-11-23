using Shop.Domain.Entities;
using Shop.Domain.Repositories;

namespace Shop.Domain.Tests.Repositories
{
    public class FakeOrderRepository : IOrderRepository
    {
        public void Save(Order order)
        {
        }
    }
}
