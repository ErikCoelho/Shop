using Shop.Domain.Entities;

namespace Shop.Domain.Repositories
{
    public interface IOrderRepository
    {
        void Save(Order order);

        IEnumerable<Order> GetAll(string customer);
    }
}
