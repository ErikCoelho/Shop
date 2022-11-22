namespace Shop.Domain.Entities
{
    public class Product: Entity
    {
        public Product(string title, string description, decimal price)
        {
            Title = title;
            Description = description;
            Price = price;
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }

    }
}
