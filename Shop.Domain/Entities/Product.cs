namespace Shop.Domain.Entities
{
    public class Product : Entity
    {
        protected Product() { }
        public Product(string image,string title, string description, decimal price, string active)
        {
            Title = title;
            Description = description;
            Price = price;
            Active = active == "true" ? true : false;
            Image = image;
        }

        public string Image { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public bool Active { get; private set; }


        public void UpdateProduct(string image, string title, string description, decimal price, string active)
        {
            Image = image;
            Title = title;
            Description = description;
            Price = price;
            Active = active == "true" ? true : false;
        }

    }
}
