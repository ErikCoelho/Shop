using Shop.Domain.Entities;
using Shop.Domain.Infra.Contexts;
using Shop.Domain.Queries;
using Shop.Domain.Repositories;

namespace Shop.Domain.Infra.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;
        
        public OrderRepository(DataContext context)
        {
            _context = context;
        }

        //public IEnumerable<Order> GetAll(string customer)
        //{
        //    return _context.Orders.AsQueryable().Where(OrderQueries.GetAll(customer)).OrderBy(x => x.Date);
        //}

        public IEnumerable<Order> GetAll(string customer)
        {
            return _context.Orders.Where(x => x.CustomerDoc == customer).OrderBy(x => x.Date);
        }

        public void Save(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
    }
}
