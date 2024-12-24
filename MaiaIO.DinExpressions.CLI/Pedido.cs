

namespace MaiaIO.DinExpressions.CLI
{
    public class Pedido
    {
        public long Id { get; set; }
        public string NomeCliente { get; set; }
        public DateTime DataCompra { get; set; }
        public bool IsActive { get; set; }
    }

    public class ListarPedidoComando
    {
        public long Id { get; set; }
        public string NomeCliente { get; set; }
        public DateTime DataCompra { get; set; }
        public bool IsActive { get; set; }
    }
}
