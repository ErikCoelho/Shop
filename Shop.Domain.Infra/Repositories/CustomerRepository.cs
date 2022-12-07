using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Domain.Infra.Contexts;
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

        public Customer Get(string doc)
        {
            return _context.Customers.FirstOrDefault(x => x.Document.Number == doc)!;
        }

        public Customer GetEmail(string email)
        {
            return _context.Customers.AsNoTracking().Include(x => x.Roles).FirstOrDefault(x => x.Email.Address == email)!;
        }
    }
}
