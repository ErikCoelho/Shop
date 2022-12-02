using Flunt.Notifications;
using Flunt.Validations;

namespace Shop.Domain.ValueObjects
{
    public class Email : Notifiable
    {

        protected Email() { }
        public Email(string address)
        {
            Address = address;

            AddNotifications(new Contract()
                .Requires()
                .IsEmail(Address, "Email.Address", "E-mail inválido")
            );
        }

        public string Address { get; private set; }
    }
}
