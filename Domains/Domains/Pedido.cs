namespace Domains.Domains
{
    public class Pedido
    {
        public string PedidoId { get; set; }
        public string Cliente { get; set; }
        public string Restaurante { get; set; }
        public List<ItemPedido> Itens { get; set; }
    }

    public class ItemPedido
    {
        public string Nome { get; set; }
        public int Quantidade { get; set; }
    }

}
