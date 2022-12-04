using Flunt.Validations;
using Shop.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Domain.Entities
{
    public class Order : Entity
    {
        protected Order() { }
        // private readonly IList<OrderItem> _items;
        public Order(string customerDoc, decimal deliveryFee)
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .IsNotNull(customerDoc, "Customer", "Cliente inválido")
            );

            CustomerDoc = customerDoc;
            Date = DateTime.Now;
            Number = Guid.NewGuid().ToString().Substring(0, 8);
            Status = EOrderStatus.WaitingPayment;
            Items = new List<OrderItem>();
            DeliveryFee = deliveryFee;
            TotalOrder = Total();
        }

        public string CustomerDoc { get; private set; }
        public DateTime Date { get; private set; }
        public string Number { get; private set; }
        public decimal DeliveryFee { get; private set; }
        // public IReadOnlyCollection<OrderItem> Items { get { return _items.ToArray(); } }
        public IList<OrderItem> Items { get; private set; }
        public decimal TotalOrder { get; private set; }
        public EOrderStatus Status { get; private set; }

        public void AddItem(Product product, int quantity)
        {
            var item = new OrderItem(product, quantity);
            if (item.Valid)
                Items.Add(item);
        }

        public decimal Total()
        {
            decimal total = 0;
            foreach (var item in Items)
            {
                total += item.Total();
            }
            total += DeliveryFee;
            TotalOrder = total;

            return total;
        }

        public void Pay(decimal amount)
        {
            if (amount == Total())
                Status = EOrderStatus.WaitingDelivery;
        }

        public void Cancel()
        {
            Status = EOrderStatus.Canceled;
        }

    }
}
