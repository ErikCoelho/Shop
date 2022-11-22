using Flunt.Validations;
using Shop.Domain.Enums;
using System.Linq;

namespace Shop.Domain.Entities
{
    public class Order : Entity
    {
        private IList<OrderItem> _items;

        public Order(Customer customer, decimal deliveryFee)
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .IsNotNull(customer, "Customer", "Cliente inválido")
                    .IsGreaterThan(deliveryFee, 0, "DeliveryFee", "O valor deve ser maior que zero")
            );

            Customer = customer;
            Date = DateTime.Now;
            Number = Guid.NewGuid().ToString().Replace("-", "").Substring(0,8);
            Status = EOrderStatus.WaitingPayment;
            _items = new List<OrderItem>();
            DeliveryFee = deliveryFee;
        }

        public Customer Customer { get; private set; }
        public DateTime Date { get; private set; }
        public string Number { get; private set; }
        public decimal DeliveryFee { get; private set; }
        public IReadOnlyCollection<OrderItem> Items { get { return _items.ToArray(); } }
        public EOrderStatus Status { get; private set; }

        public void AddItem(Product product, int quantity)
        {
            var item = new OrderItem(product, quantity);
            if(item.Valid)
                _items.Add(item);
        }

        public decimal Total()
        {
            decimal total = 0;
            foreach(var item in Items)
            {
                total += item.Total();
            }

            total += DeliveryFee;

            return total;
        }

        public void Pay(decimal amount)
        {
            if (amount == Total())
                Status = EOrderStatus.WaitingDelivery;
        }

        public void Cancel()
        {
            if (Status == EOrderStatus.WaitingDelivery)
            {
                return;
            }

            Status = EOrderStatus.Canceled;
        }
    }
}
