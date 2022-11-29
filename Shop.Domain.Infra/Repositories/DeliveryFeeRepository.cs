using Shop.Domain.Repositories;

namespace Shop.Domain.Infra.Repositories
{
    public class DeliveryFeeRepository : IDeliveryFeeRepository
    {
        public decimal Get(string zipCode)
        {
            return 10;
        }
    }
}
