using Flunt.Notifications;
using Flunt.Validations;
using Shop.Domain.Commands.Contracts;

namespace Shop.Domain.Commands.Product
{
    public class EditProductCommand : Notifiable, ICommand
    {
        public EditProductCommand() { }

        public EditProductCommand(string image, string title, string description, string price, string active)
        {
            Title = title;
            Description = description;
            Price = decimal.Parse(price);
            Active = active;
            Image = image;
        }

        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Active { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(Title, 3, "Title", "O título deve conter no mínimo 3 caracteres")
                .HasMinLen(Description, 3, "Description", "A descrição deve conter no mínimo 3 caracteres")
                .IsGreaterThan(Price, 0, "Price", "O preço deve ser maior que 0")
                );
        }
    }
}
