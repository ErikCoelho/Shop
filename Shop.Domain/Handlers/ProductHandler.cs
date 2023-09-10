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

        public async Task<ICommandResult> HandleAsync(EditProductCommand command)
        {
            command.Validate();
            if(command.Invalid)
                return new GenericCommandResult(false, "Produto inválido", Notifications);

            var product = new Product(command.Image, command.Title, command.Description, command.Price, command.Active);

            AddNotifications(product.Notifications);

            if (Invalid)
                return new GenericCommandResult(false, "Falha ao criar o produto", Notifications);

            await _productRepository.CreateAsync(product);
            return new GenericCommandResult(true, $"Produto {product.Id} criado com sucesso", product);
        }

        public async Task<ICommandResult> HandleEditAsync(EditProductCommand command, Guid id)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Produto inválido", Notifications);

            var product =  await _productRepository.GetByIdAsync(id);
            if(product == null)
                return new GenericCommandResult(false, "O produto não existe", Notifications);

            product.UpdateProduct(command.Image, command.Title, command.Description, command.Price, command.Active);

            AddNotifications(product.Notifications);

            if (Invalid)
                return new GenericCommandResult(false, "Falha ao atualizar o produto", Notifications);

            await _productRepository.UpdateAsync(product);
            return new GenericCommandResult(true, $"Produto {product.Id} atualizado com sucesso", product);
        }

        public async Task<ICommandResult> HandleDeleteAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return new GenericCommandResult(false, "O produto não existe", Notifications);

            await _productRepository.DeleteAsync(product);
            return new GenericCommandResult(true, $"Produto {product.Id} deletado com sucesso", product);
        }
    }
}
