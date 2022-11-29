using Flunt.Notifications;
using Shop.Domain.Commands;
using Shop.Domain.Commands.Contracts;
using Shop.Domain.Entities;
using Shop.Domain.Handlers.Contracts;
using Shop.Domain.Repositories;
using Shop.Domain.ValueObjects;

namespace Shop.Domain.Handlers
{
    public class CustomerHandler : Notifiable, IHandler<CreateCustomerCommand>
    {
        private readonly ICustomerRepository _repository;

        public CustomerHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public ICommandResult Handle(CreateCustomerCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Usuário inválido", Notifications);
            
            var name = new Name(command.FirstName, command.LastName);
            var doc = new Document(command.Document);
            var email = new Email(command.Email);
            var customer = new Customer(name, doc,email, command.PasswordHash);

            AddNotifications(customer);

            if (Invalid)
                return new GenericCommandResult(false, "Falha ao criar usuário", Notifications);
            
            _repository.Create(customer);
            return new GenericCommandResult(true, "Usuário criado", customer);
            
        }
    }
}
