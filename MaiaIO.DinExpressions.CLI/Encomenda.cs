

namespace MaiaIO.DinExpressions.CLI
{
    public class Encomenda
    {
        public int Id { get; set; }
        public string EmpresaOrigem { get; set; }
        public string EmpresaDestino { get; set; }
        public DateTime PrevisaoChegada { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool IsActive { get; set; }
    }

    public class ListarEncomedaComando
    {
        public int Id { get; set; }
        public string EmpresaOrigem { get; set; }
        public string EmpresaDestino { get; set; }
        public DateTime PrevisaoChegada { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool IsActive { get; set; }
    }
}
