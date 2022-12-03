using Shop.Domain.Commands;
using Shop.Domain.Commands.Customer;
using Shop.Domain.Handlers;
using Shop.Domain.Tests.Repositories;

namespace Shop.Domain.Tests.Handlers
{
    [TestClass]
    public class CustomerHandlerTest
    {
        private CustomerHandler _handler = new(new FakeCustomerRepository());
        private readonly CreateCustomerCommand _command = new();

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_dados_incorretos_o_usuario_nao_devera_ser_criado()
        {
            _command.FirstName = "Erik";
            _command.LastName = "";
            _command.Document = "";
            _command.Email = "test@email.com";
            _handler.Handle(_command);
            var result = (GenericCommandResult)_handler.Handle(_command);
            Assert.AreEqual(result.Success, false);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_dados_corretos_o_usuario_devera_ser_criado()
        {
            _command.FirstName = "Erik";
            _command.LastName = "Coelho";
            _command.Document = "19766465029";
            _command.Email = "test@email.com";
            _handler.Handle(_command);
            var result = (GenericCommandResult)_handler.Handle(_command);
            Assert.AreEqual(result.Success, true);
        }

    }
}
