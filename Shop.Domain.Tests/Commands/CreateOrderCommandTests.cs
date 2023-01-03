using Shop.Domain.Commands;
using Shop.Domain.Commands.Order;
using Shop.Domain.Commands.OrderItem;

namespace Shop.Domain.Tests.Commands
{
    [TestClass]
    public class CreateOrderCommandTests
    {
        private readonly CreateOrderCommand _command = new();

        [TestMethod]
        [TestCategory("Commands")]
        public void Dado_um_comand_invalido_o_pedido_nao_deve_ser_gerado()
        {
            _command.ZipCode = "12345678";
            _command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 0));
            _command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 0));
            _command.Validate();

            Assert.AreEqual(_command.Valid, false);
        }

        [TestMethod]
        [TestCategory("Commands")]
        public void Dado_um_comand_valido_o_pedido_deve_ser_gerado()
        {
            _command.ZipCode = "12345678";
            _command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            _command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            _command.Validate();

            Assert.AreEqual(_command.Valid, true);
        }

        [TestMethod]
        [TestCategory("Commands")]
        public void Dado_um_comand_sem_itens_o_pedido_nao_deve_ser_gerado()
        {
            _command.ZipCode = "12345678";
            _command.Validate();

            Assert.AreEqual(_command.Valid, false);
        }
    }
}
