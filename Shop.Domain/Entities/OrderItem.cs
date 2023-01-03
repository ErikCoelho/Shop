using Flunt.Validations;

namespace Shop.Domain.Entities
{
    public class OrderItem : Entity
    {
        protected OrderItem() { }
        public OrderItem(Guid product, int quantity, decimal price)
        {
            AddNotifications(new Contract()
                    .Requires()
                    .IsNotNull(product, "Product", "Produto inválido")
                    .IsGreaterThan(quantity, 0, "Quantity", "A quantidade deve ser maior que zero")
                    );

            Product = product;
            Price = price;
            Quantity = quantity;
        }

        public Guid Product { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }

        public decimal Total()
        {
            return Price * Quantity;
        }
    }
}
