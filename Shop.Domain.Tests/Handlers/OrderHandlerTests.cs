using Shop.Domain.Commands;
using Shop.Domain.Commands.Order;
using Shop.Domain.Commands.OrderItem;
using Shop.Domain.Handlers;
using Shop.Domain.Repositories;
using Shop.Domain.Tests.Repositories;

namespace Shop.Domain.Tests.Handlers
{
    [TestClass]
    public class OrderHandlerTests
    {
        private readonly CreateOrderCommand _command = new();

        private readonly ICustomerRepository _customerRepository;
        private readonly IDeliveryFeeRepository _deliveryFeeRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderHandlerTests()
        {
            _customerRepository = new FakeCustomerRepository();
            _deliveryFeeRepository = new FakeDeliveryFeeRepository();
            _orderRepository = new FakeOrderRepository();
            _productRepository = new FakeProductRepository();
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public async Task Dado_um_cliente_inexistente_o_pedido_nao_deve_ser_gerado()
        {
            _command.ZipCode = "12345678";
            _command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            _command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

            var handler = new OrderHandler(
                _customerRepository,
                _deliveryFeeRepository,
                _productRepository,
                _orderRepository);

            var result = (GenericCommandResult) await handler.HandleOrder(_command, String.Empty);
            Assert.AreEqual(result.Success, false);
        }
    }
}
