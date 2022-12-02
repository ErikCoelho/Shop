using Flunt.Notifications;
using Flunt.Validations;

namespace Shop.Domain.ValueObjects
{
    public class Name : Notifiable
    {
        protected Name() { }
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(FirstName, 3, "Name.FirstName", "Nome deve conter pelo menos 3 caracteres")
                .HasMinLen(LastName, 3, "Name.LastName", "Sobrenome deve conter pelo menos 3 caracteres")
                .HasMaxLen(FirstName, 30, "Name.FirstName", "Nome deve conter até 30 caracteres")
                .HasMaxLen(LastName, 30, "Name.LastName", "Sobrenome deve conter até 30 caracteres")
                );
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
