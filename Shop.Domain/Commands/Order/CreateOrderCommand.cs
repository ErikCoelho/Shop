using Flunt.Notifications;
using Flunt.Validations;
using Shop.Domain.Commands.Contracts;
using Shop.Domain.Commands.OrderItem;

namespace Shop.Domain.Commands.Order
{
    public class CreateOrderCommand : Notifiable, ICommand
    {
        public CreateOrderCommand()
        {
            Items = new List<CreateOrderItemCommand>();
        }

        public CreateOrderCommand(string? zipCode, IList<CreateOrderItemCommand> items)
        {
            ZipCode = zipCode;
            Items = items;
        }

        public string ZipCode { get; set; } = "12345678";
        public IList<CreateOrderItemCommand> Items { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsGreaterThan(Items.Count, 0, "Items", "Nenhum item do pedido foi encontrado")
                );
        }
    }
}
