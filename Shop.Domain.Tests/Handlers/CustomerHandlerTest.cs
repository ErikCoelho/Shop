using Shop.Domain.Commands;
using Shop.Domain.Commands.Customer;
using Shop.Domain.Handlers;
using Shop.Domain.Services;
using Shop.Domain.Tests.Repositories;

namespace Shop.Domain.Tests.Handlers
{
    [TestClass]
    public class CustomerHandlerTest
    {
        private CustomerHandler _handler = new(new FakeCustomerRepository(), null);
        private readonly CreateCustomerCommand _command = new();

        [TestMethod]
        [TestCategory("Handlers")]
        public async Task Dado_dados_incorretos_o_usuario_nao_deve_ser_criado()
        {
            _command.FirstName = "Erik";
            _command.LastName = "";
            _command.Document = "";
            _command.Email = "test@email.com";
            var result = (GenericCommandResult)await _handler.HandleAsync(_command);
            Assert.AreEqual(result.Success, false);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public async Task Dado_dados_corretos_o_usuario_deve_ser_criado()
        {
            _command.FirstName = "Erik";
            _command.LastName = "Coelho";
            _command.Document = "19766465029";
            _command.Email = "test@email.com";
            _command.PasswordHash = "senha123";
            var result = (GenericCommandResult) await _handler.HandleAsync(_command);
            Assert.AreEqual(result.Success, true);
        }

    }
}
