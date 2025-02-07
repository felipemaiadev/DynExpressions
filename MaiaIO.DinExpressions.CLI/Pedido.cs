

using System.Reflection;

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

    public class NestedEntity : Attribute
    {

        public NestedEntity(Type type, string nestedCollectionName, string property)
        {
        }
    }

        public class ListarPedidoComando
    {
        public long Id { get; set; }
        
        public string NomeCliente { get; set; }
      
        public DateTime DataCompra { get; set; }
        
        public bool IsActive { get; set; }

        [NestedEntity(typeof(Produto), "Produtos", "Id")]
        public List<long> Produtos {  get; set; } 
    }
}
