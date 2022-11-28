using Shop.Domain.Commands;
using Shop.Domain.Handlers;
using Shop.Domain.Repositories;
using Shop.Domain.Tests.Repositories;

namespace Shop.Domain.Tests.Handlers
{
    [TestClass]
    public class OrderHandlerTests
    {
        private static readonly CreateOrderCommand _command = new();

        private readonly ICustomerRepository _customerRepository;
        private readonly IDeliveryFeeRepository _deliveryFeeRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderHandlerTests()
        {
            _customerRepository = new FakeCustomerRepository();
            _deliveryFeeRepository = new FakeDeliveryFeeRepository();
            _productRepository = new FakeProductRepository();
            _orderRepository = new FakeOrderRepository();
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_cliente_inexistente_o_pedido_nao_deve_ser_gerado()
        {
            _command.Customer = "";
            _command.ZipCode = "12345678";
            _command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            _command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            
            var handler = new OrderHandler(
                _customerRepository,
                _deliveryFeeRepository,
                _productRepository,
                _orderRepository);

            var result = (GenericCommandResult)handler.Handle(_command);
            Assert.AreEqual(result.Success, false);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_cep_invalido_o_pedido_deve_ser_gerado_normalmente()
        {
            _command.Customer = "59493843009";
            _command.ZipCode = "";
            _command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            _command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            var handler = new OrderHandler(
                _customerRepository,
                _deliveryFeeRepository,
                _productRepository,
                _orderRepository);

            handler.Handle(_command);
            Assert.AreEqual(handler.Valid, true);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_pedido_sem_itens_o_mesmo_nao_deve_ser_gerado()
        {
            _command.Customer = "59493843009";
            _command.ZipCode = "49090600";

            var handler = new OrderHandler(
                _customerRepository,
                _deliveryFeeRepository,
                _productRepository,
                _orderRepository);

            var result = (GenericCommandResult)handler.Handle(_command);
            Assert.AreEqual(result.Success, false);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_comando_invalido_o_pedido_nao_deve_ser_gerado()
        {
            var command = new CreateOrderCommand();
            command.Customer = "";
            command.ZipCode = "";
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

            var handler = new OrderHandler(
               _customerRepository,
               _deliveryFeeRepository,
               _productRepository,
               _orderRepository);

            var result = (GenericCommandResult)handler.Handle(_command);
            Assert.AreEqual(result.Success, false);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_comando_valido_o_pedido_deve_ser_gerado()
        {
            _command.Customer = "59493843009";
            _command.ZipCode = "49090600";
            _command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 2));
            _command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

            var handler = new OrderHandler(
                _customerRepository,
                _deliveryFeeRepository,
                _productRepository,
                _orderRepository);

            handler.Handle(_command);
            Assert.AreEqual(handler.Valid, true);
        }
    }
}
