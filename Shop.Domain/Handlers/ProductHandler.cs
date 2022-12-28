using Flunt.Notifications;
using Shop.Domain.Commands;
using Shop.Domain.Commands.Contracts;
using Shop.Domain.Commands.Product;
using Shop.Domain.Entities;
using Shop.Domain.Handlers.Contracts;
using Shop.Domain.Repositories;

namespace Shop.Domain.Handlers
{
    public class ProductHandler : Notifiable, IHandler<EditProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public ProductHandler(IProductRepository repository)
        {
            _productRepository = repository;
        }

        public ICommandResult Handle(EditProductCommand command)
        {
            command.Validate();
            if(command.Invalid)
                return new GenericCommandResult(false, "Produto inválido", Notifications);

            var product = new Product(command.Image, command.Title, command.Description, command.Price, command.Active);

            AddNotifications(product.Notifications);

            if (Invalid)
                return new GenericCommandResult(false, "Falha ao criar o produto", Notifications);

            _productRepository.Create(product);
            return new GenericCommandResult(true, $"Produto {product.Id} criado com sucesso", product);
        }

        public ICommandResult HandleEdit(EditProductCommand command, Guid id)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Produto inválido", Notifications);

            var product =  _productRepository.GetById(id);
            if(product == null)
                return new GenericCommandResult(false, "O produto não existe", Notifications);

            product.UpdateCustomer(command.Image, command.Title, command.Description, command.Price, command.Active);

            AddNotifications(product.Notifications);

            if (Invalid)
                return new GenericCommandResult(false, "Falha ao atualizar o produto", Notifications);

            _productRepository.Update(product);
            return new GenericCommandResult(true, $"Produto {product.Id} atualizado com sucesso", product);
        }

        public ICommandResult HandleDelete(Guid id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
                return new GenericCommandResult(false, "O produto não existe", Notifications);

            _productRepository.Delete(product);
            return new GenericCommandResult(true, $"Produto {product.Id} deletado com sucesso", product);
        }
    }
}
