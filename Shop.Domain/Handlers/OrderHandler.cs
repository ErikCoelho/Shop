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

        public ICommandResult Handle(CreateOrderCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Pedido inválido", Notifications);

            var customer = _customerRepository.Get(command.Customer);
            //var customerVo = new Customer(customer.Name, customer.Document, customer.Email, customer.PasswordHash);
            var deliveryFee = _deliveryFeeRepository.Get(command.ZipCode);
            var products = _productRepository.Get(ExtractGuids.Extract(command.Items)).ToList();
            var order = new Order(customer.Document.Number, deliveryFee);
            
            foreach(var item in command.Items)
            {
                var product = products.Where(x => x.Id == item.Product).FirstOrDefault();
                order.AddItem(product, item.Quantity);
            }

            AddNotifications(order.Notifications);

            if (Invalid)
                return new GenericCommandResult(false, "Falha ao gerar o pedido", Notifications);


            _orderRepository.Save(order);
            return new GenericCommandResult(true, $"Pedido {order.Number} gerado com sucesso", order);

        }
    }
}
