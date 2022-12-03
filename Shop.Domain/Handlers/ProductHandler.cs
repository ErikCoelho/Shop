using Flunt.Notifications;
using Shop.Domain.Commands;
using Shop.Domain.Commands.Contracts;
using Shop.Domain.Commands.Product;
using Shop.Domain.Entities;
using Shop.Domain.Handlers.Contracts;
using Shop.Domain.Repositories;

namespace Shop.Domain.Handlers
{
    public class ProductHandler : Notifiable, IHandler<CreateProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public ProductHandler(IProductRepository repository)
        {
            _productRepository = repository;
        }

        public ICommandResult Handle(CreateProductCommand command)
        {
            command.Validate();
            if(command.Invalid)
                return new GenericCommandResult(false, "Produto inválido", Notifications);

            var product = new Product(command.Title, command.Description, command.Price, command.Active);

            AddNotifications(product.Notifications);

            if (Invalid)
                return new GenericCommandResult(false, "Falha ao criar o produto", Notifications);

            _productRepository.Create(product);
            return new GenericCommandResult(true, $"Produto {product.Id} criado com sucesso", product);
        }
    }
}
