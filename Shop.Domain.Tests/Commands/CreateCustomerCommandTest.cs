using Shop.Domain.Commands.Customer;

namespace Shop.Domain.Tests.Commands
{
    [TestClass]
    public class CreateCustomerCommandTest
    {
        private readonly CreateCustomerCommand _command = new();

        [TestMethod]
        [TestCategory("Commands")]
        public void Dado_um_comando_invalido_o_usuario_nao_deve_ser_criado()
        {
            _command.FirstName = "Erik";
            _command.LastName = "";
            _command.Document = "";
            _command.Email = "test@email.com";
            _command.Validate();
            Assert.AreEqual(_command.Valid, false);
        }

        [TestMethod]
        [TestCategory("Commands")]
        public void Dado_um_comando_valido_o_usuario_deve_ser_criado()
        {
            _command.FirstName = "Erik";
            _command.LastName = "Coelho";
            _command.Document = "19766465029";
            _command.Email = "test@email.com";
            _command.Validate();
            Assert.AreEqual(_command.Valid, true);
        }
    }
}
