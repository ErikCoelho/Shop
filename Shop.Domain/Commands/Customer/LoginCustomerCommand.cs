using Flunt.Notifications;
using Flunt.Validations;
using Shop.Domain.Commands.Contracts;

namespace Shop.Domain.Commands.Customer
{
    public class LoginCustomerCommand : Notifiable, ICommand
    {
        public LoginCustomerCommand() { }
        public LoginCustomerCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; }
        public string Password { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsEmail(Email, "Email", "O E-mail é inválido")
                .IsNotNull(Password, "Password", "A senha é obrigatória")
                );
        }
    }
}
