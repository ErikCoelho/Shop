using Flunt.Notifications;
using SecureIdentity.Password;
using Shop.Domain.Commands;
using Shop.Domain.Commands.Contracts;
using Shop.Domain.Commands.Customer;
using Shop.Domain.Entities;
using Shop.Domain.Handlers.Contracts;
using Shop.Domain.Repositories;
using Shop.Domain.Services;
using Shop.Domain.ValueObjects;

namespace Shop.Domain.Handlers
{
    public class CustomerHandler : Notifiable, IHandler<CreateCustomerCommand>
    {
        private readonly ICustomerRepository _repository;
        private readonly TokenService _tokenService;

        public CustomerHandler(ICustomerRepository repository, TokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
        }

        public async Task<ICommandResult> HandleAsync(CreateCustomerCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Usuário inválido", Notifications);
            
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document);
            var email = new Email(command.Email);
            var password = PasswordHasher.Hash(command.PasswordHash);
            var customer = new Customer(name, document,email, password);

            AddNotifications(customer);

            if (Invalid)
                return new GenericCommandResult(false, "Falha ao criar usuário", Notifications);
            
            await _repository.CreateAsync(customer);
            return new GenericCommandResult(true, "Usuário criado", customer);

        }
        
        public async Task<ICommandResult> HandleLoginAsync(LoginCustomerCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Usuário ou senha inválidos", Notifications);

            var customer = await _repository.GetEmailAsync(command.Email);
            if (customer == null)
            {
                return new GenericCommandResult(false, "Usuário ou senha inválidos", Notifications);
            }

            if(!PasswordHasher.Verify(customer.PasswordHash, command.Password))
                return new GenericCommandResult(false, "Usuário ou senha inválidos", Notifications);

            var token = _tokenService.GenerateToken(customer);

            return new GenericCommandResult(true, "Usuário logado", token);
        }
    }
}
