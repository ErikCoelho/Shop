using Microsoft.EntityFrameworkCore;
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
        //    return _context.Orders.AsNoTracking().Where(OrderQueries.GetAll(customer));
        //}

        public IEnumerable<Order> GetAll(string customer)
        {
            return _context.Orders.AsNoTracking().Where(x => x.CustomerDoc == customer).Include(x =>x.Items).ToList();
        }

        public void Save(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
    }
}
