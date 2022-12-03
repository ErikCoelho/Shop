using Flunt.Notifications;
using Flunt.Validations;
using Shop.Domain.Commands.Contracts;

namespace Shop.Domain.Commands.Product
{
    public class CreateProductCommand : Notifiable, ICommand
    {
        public CreateProductCommand() { }

        public CreateProductCommand(string title, string description, decimal price, bool active)
        {
            Title = title;
            Description = description;
            Price = price;
            Active = active;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(Title, 3, "Title", "O título deve conter no mínimo 3 caracteres")
                .HasMinLen(Description, 3, "Description", "A descrição deve conter no mínimo 3 caracteres")
                .IsGreaterThan(Price, 0, "Price", "Nenhum item do pedido foi encontrado")
                );
        }
    }
}
