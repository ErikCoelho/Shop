namespace Shop.Domain.Entities
{
    public class Product : Entity
    {
        protected Product() { }
        public Product(string title, string description, decimal price, bool active)
        {
            Title = title;
            Description = description;
            Price = price;
            Active = active;
        }

        public string Image { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public bool Active { get; private set; }

    }
}
