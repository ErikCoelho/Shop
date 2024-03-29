﻿using Shop.Domain.ValueObjects;

namespace Shop.Domain.Entities
{
    public class Customer: Entity
    {
        protected Customer() { }

        public Customer(Name name, Document document, Email email, string passwordHash)
        {
            Name = name;
            Document = document;
            Email = email;
            Slug= name.ToString().ToLower().Replace(" ", "-");
            PasswordHash = passwordHash;

            AddNotifications(Name, Email, Document);
        }

        public string Image { get; set; }
        public Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public string Slug { get; private set; }
        public string PasswordHash { get; private set; }

        public IList<Role> Roles { get; set; }

    }

}
