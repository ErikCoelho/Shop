using Shop.Domain.ValueObjects;

namespace Shop.Domain.Tests.ValueObjects
{
    [TestClass]
    public class DocumentTests
    {
        [TestMethod]
        public void Deve_retornar_erro_quando_cpf_for_invalido()
        {
            var doc = new Document("123");
            Assert.IsTrue(doc.Invalid);
        }

        [TestMethod]
        public void Deve_retornar_sucesso_quando_cpf_for_valido()
        {
            var doc = new Document("484.329.160-99");
            Assert.IsTrue(doc.Valid);
        }
    }
}
