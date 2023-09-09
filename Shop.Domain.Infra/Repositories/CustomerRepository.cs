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

        public async Task CreateAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<Customer> GetAsync(string document)
        {
            return await _context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Document.Number == document)!;
        }

        public async Task<Customer> GetEmailAsync(string email)
        {
            return await _context.Customers.AsNoTracking().Include(x => x.Roles).FirstOrDefaultAsync(x => x.Email.Address == email)!;
        }
    }
}
