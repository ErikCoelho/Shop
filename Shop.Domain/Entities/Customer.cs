using Shop.Domain.ValueObjects;

namespace Shop.Domain.Entities
{
    public class Customer: Entity
    {
        public Customer(Name name, Document document, Email email, string passwordHash)
        {
            Name = name;
            Document = document;
            Email = email;
            Slug = email.ToString().Replace("@", "-").Replace(".", "-");
            PasswordHash = passwordHash;

            AddNotifications(Name, Email, Document);
        }

        public Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public string Slug { get; private set; }
        public string PasswordHash { get; private set; }
    
    }

}
