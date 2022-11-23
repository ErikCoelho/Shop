using Shop.Domain.Entities;
using Shop.Domain.ValueObjects;

namespace Shop.Domain.Tests.Entities
{
    [TestClass]
    public class OrderTests
    {
        private static readonly Name _name = new("Erik", "Coelho");
        private static readonly Document _doc = new ("098.952.210-53");
        private static readonly Email _email = new ("eriktest@gmail.com");
        private static readonly Product _product = new ("Iphone", "Novo iphone xs 110 ultra max", 55, true);
        private static readonly Customer _customer = new (_name, _doc, _email, "senha123");
        private static readonly Order _order = new (_customer, 5);
        
        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_novo_item_sem_produto_o_mesmo_nao_deve_ser_adicionado()
        {
            _order.AddItem(null, 2);
            Assert.AreEqual(_order.Items.Count, 0);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_novo_item_com_quantidade_zero_ou_menor_o_mesmo_nao_deve_ser_adicionado()
        {
            _order.AddItem(_product, 0);
            Assert.AreEqual(_order.Items.Count, 0);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_novo_pedido_valido_seu_total_deve_ser_60()
        {
            _order.AddItem(_product, 1);
            Assert.AreEqual(_order.Total(), 60);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_novo_pedido_sem_cliente_o_mesmo_deve_ser_invalido()
        {
            var order = new Order(null, 5);
            Assert.IsTrue(order.Invalid);
        }
    }
}
