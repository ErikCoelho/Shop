using Shop.Domain.Entities;
using Shop.Domain.Infra.Contexts;
using Shop.Domain.Queries;
using Shop.Domain.Repositories;

namespace Shop.Domain.Infra.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext _context;

        public CustomerRepository(DataContext context)
        {
            _context = context;
        }

        public void Create(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        //public Customer Get(string doc)
        //{
        //    return (Customer)_context.Customers.AsQueryable().Where(CustomerQueries.Get(doc));
        //}

        public Customer Get(string doc)
        {
            return _context.Customers.FirstOrDefault(x => x.Document.Number == doc);
        }
    }
}
