using Shop.Domain.Entities;

namespace Shop.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task SaveAsync(Order order);

        Task<IEnumerable<Order>> GetAllAsync(string customer);
    }
}
