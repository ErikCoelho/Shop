using Shop.Domain.Commands;
using Shop.Domain.Commands.Order;
using Shop.Domain.Commands.OrderItem;
using Shop.Domain.Commands.Product;
using Shop.Domain.Entities;
using Shop.Domain.Handlers;
using Shop.Domain.Repositories;
using Shop.Domain.Tests.Repositories;

namespace Shop.Domain.Tests.Handlers
{
    [TestClass]
    public class OrderHandlerTests
    {
        private readonly CreateOrderCommand _command = new();
        private readonly Product _produt = new(null, "Celular", "hello world", 50, "true");

        private readonly OrderHandler _handler = new(
            new FakeCustomerRepository(),
            new FakeDeliveryFeeRepository(),
            new FakeProductRepository(),
            new FakeOrderRepository()
        );

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_cliente_inexistente_o_pedido_nao_deve_ser_gerado()
        {
            _command.ZipCode = "12345678";
            _command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            _command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            var result = (GenericCommandResult)_handler.HandleOrder(_command, "");
            Assert.AreEqual(result.Success, false);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_cep_invalido_o_pedido_deve_ser_gerado_normalmente()
        {
            _command.ZipCode = "";
            _command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            _command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            var result = (GenericCommandResult)_handler.HandleOrder(_command, "59493843009");
            Assert.AreEqual(result.Success, true);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_pedido_sem_itens_o_mesmo_nao_deve_ser_gerado()
        {
            _command.ZipCode = "49090600";
            var result = (GenericCommandResult)_handler.HandleOrder(_command, "59493843009");
            Assert.AreEqual(result.Success, false);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_comando_invalido_o_pedido_nao_deve_ser_gerado()
        {
            _command.ZipCode = "";
            _command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            _command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            var result = (GenericCommandResult)_handler.HandleOrder(_command, "");
            Assert.AreEqual(result.Success, false);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_comando_valido_o_pedido_deve_ser_gerado()
        {
            _command.ZipCode = "49090600";
            _command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 2));
            _command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            var result = (GenericCommandResult)_handler.HandleOrder(_command, "59493843009");
            Assert.AreEqual(result.Success, true);
        }
    }
}
