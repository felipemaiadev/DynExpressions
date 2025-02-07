using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaiaIO.DinExpressions.CLI.Enums
{
    public enum StatusPedidoEnum
    {
        [Description("Pendente Faturamento")]
        PendenteFaturamento = 0x0001,

        [Description("Pendente Movimentacao Estoque")]
        PendenteMovimentacao = 0x0010,

        [Description("Pendente Confirmacao Pagamento")]
        PendentePagamento = 0x0100,

        [Description("Pendente Embarque e Expedicao")]
        PendenteExpedicao = 0x1000,

        [Description("Pedido Finalizado")]
        Finalizado = 0x1111,
    }
}
