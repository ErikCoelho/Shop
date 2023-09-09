using Flunt.Notifications;
using Shop.Domain.Commands;
using Shop.Domain.Commands.Contracts;
using Shop.Domain.Commands.Order;
using Shop.Domain.Entities;
using Shop.Domain.Handlers.Contracts;
using Shop.Domain.Repositories;
using Shop.Domain.Utils;

namespace Shop.Domain.Handlers
{
    public class OrderHandler : Notifiable, IHandler<CreateOrderCommand>
    {

        private readonly ICustomerRepository _customerRepository;
        private readonly IDeliveryFeeRepository _deliveryFeeRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderHandler(
            ICustomerRepository customerRepository,
            IDeliveryFeeRepository deliveryFeeRepository,
            IProductRepository productRepository,
            IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _deliveryFeeRepository = deliveryFeeRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public async Task<ICommandResult> HandleOrder(CreateOrderCommand command, string customer)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Pedido inválido", Notifications);

            var customerData = await _customerRepository.GetAsync(customer);
            if (customer == null)
                return new GenericCommandResult(false, "Usuário inválido", Notifications);

            var deliveryFee = _deliveryFeeRepository.Get(command.ZipCode);
            var productsList = await _productRepository.GetAsync(ExtractGuids.Extract(command.Items));
            var products = productsList.ToList();
            
            var order = new Order(customerData.Document.Number, deliveryFee);

            foreach (var item in command.Items)
            {
                var product = products.Where(x => x.Id == item.Product).FirstOrDefault()!;
                order.AddItem(product, item.Quantity);
            }
            order.Total();

            AddNotifications(order.Notifications);

            if (Invalid)
                return new GenericCommandResult(false, "Falha ao gerar o pedido", Notifications);


            await _orderRepository.SaveAsync(order);
            return new GenericCommandResult(true, $"Pedido {order.Number} gerado com sucesso", order);

        }

        public async Task<ICommandResult> HandleAsync(CreateOrderCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
