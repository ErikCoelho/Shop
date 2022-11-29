using Flunt.Notifications;
using Flunt.Validations;
using Shop.Domain.Commands.Contracts;

namespace Shop.Domain.Commands
{
    public class CreateOrderCommand : Notifiable, ICommand
    {
        public CreateOrderCommand()
        {
            Items = new List<CreateOrderItemCommand>();
        }

        public CreateOrderCommand(string customer, string zipCode, IList<CreateOrderItemCommand> items)
        {
            Customer = customer;
            ZipCode = zipCode;
            Items = items;
        }

        public string Customer { get; set; }
        public string ZipCode { get; set; }
        public IList<CreateOrderItemCommand> Items { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .HasLen(Customer, 11, "Customer", "Documento inválido")
                .IsGreaterThan(Items.Count, 0, "Items", "Nenhum item do pedido foi encontrado")
                );
        }
    }
}
