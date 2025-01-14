

namespace MaiaIO.DinExpressions.CLI
{
    public class Pedido
    {
        public long Id { get; set; }
        public string NomeCliente { get; set; }
        public DateTime DataCompra { get; set; }
        public IList<Produto> Produtos { get; set; }
        public bool IsActive { get; set; }
    }

    public class Produto
    {
        public long Id { get; set; }
        public decimal Price { get; set; }
        public string ProdName { get; set; }

    }

    public class ListarPedidoComando
    {
        public long Id { get; set; }
        public string NomeCliente { get; set; }
        public DateTime DataCompra { get; set; }
        public bool IsActive { get; set; }
        public int[] ProdutosId {  get; set; } 
    }
}
