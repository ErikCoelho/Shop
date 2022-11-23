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

        public string Customer { get; set; }
        public string ZipCode { get; set; }
        public IList<CreateOrderItemCommand> Items { get; set; }


        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .HasLen(Customer, 11, "Customer", "Cliente inválido")
                .HasLen(ZipCode, 8, "ZipCode", "CEP inválido")
                );
        }
    }
}
