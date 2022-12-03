using Flunt.Notifications;
using Flunt.Validations;
using Shop.Domain.Commands.Contracts;

namespace Shop.Domain.Commands.Customer
{
    public class CreateCustomerCommand : Notifiable, ICommand
    {
        public CreateCustomerCommand() { }
        public CreateCustomerCommand(string firstName, string lastName, string document, string email, string passwordHash)
        {
            FirstName = firstName;
            LastName = lastName;
            Document = document;
            Email = email;
            PasswordHash = passwordHash;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(FirstName, 3, "FistName", "O nome deve conter pelo menos 3 caracteres")
                .HasMaxLen(FirstName, 40, "FistName", "O nome deve conter no máximo 40 caracteres")
                .HasMinLen(LastName, 3, "LastName", "O sobrenome deve conter pelo menos 3 caracteres")
                .HasMaxLen(LastName, 40, "LastName", "O sobrenome deve conter no máximo 40 caracteres")
                .IsEmail(Email, "Email", "O E-mail é inválido")
                .HasLen(Document, 11, "Document", "CPF inválido")
                );
        }
    }
}
