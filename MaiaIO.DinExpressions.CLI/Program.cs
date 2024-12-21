// See https://aka.ms/new-console-template for more information
using MaiaIO.DinExpressions.CLI;

Console.WriteLine("<== TESTE pedidos ====>");


var pedidos = new List<Pedido>() {
           new Pedido { Id = 1, NomeCliente = "Channing Matthew Tatum", DataCompra = new DateTime(2024,3,1) , IsActive = true },
           new Pedido { Id = 2, NomeCliente =  "Joseph Michael Manganiello", DataCompra = new DateTime(2024, 4, 4), IsActive = false },
           new Pedido { Id = 3, NomeCliente = "Matthew Staton Bomer", DataCompra = new DateTime(2024, 1, 5), IsActive = false },
           new Pedido { Id = 4, NomeCliente = "Alexander Richard Pettyfer", DataCompra = new DateTime(2024, 1, 5), IsActive = false }
    };

var comandoPedido = new ListarPedidoComando { NomeCliente = "Manganiello" };

Func<Pedido, bool> queryPedidos = GenericExpressionBuilder<ListarPedidoComando, Pedido>.FiltroCreate<Pedido>(comandoPedido);
var resultPedido = pedidos.Where(queryPedidos).ToList();
resultPedido.ForEach(e => Console.WriteLine(e.Id));



Console.WriteLine("<== TESTE Encomendas ====>");

var encomendas = new List<Encomenda>() {
           new Encomenda { Id = 1, EmpresaOrigem = "XPTO10", EmpresaDestino = "XPTO15", DataCriacao = new DateTime(2024,3,1) , IsActive = true },
           new Encomenda { Id = 2, EmpresaOrigem = "PGTO21", EmpresaDestino = "XPTO12", DataCriacao = new DateTime(2024, 4, 4), IsActive = false },
           new Encomenda { Id = 3, EmpresaOrigem = "WHTO36", EmpresaDestino = "XPTO14", DataCriacao = new DateTime(2024, 1, 5), IsActive = false },
           new Encomenda { Id = 4, EmpresaOrigem = "XPTO90", EmpresaDestino = "XPTO28", DataCriacao = new DateTime(2024,3,1) , IsActive = true },
    };


var comando = new ListarEncomedaComando { EmpresaOrigem = "XPTO" };

Func<Encomenda, bool> queryEncomendas = GenericExpressionBuilder<ListarEncomedaComando, Encomenda>.FiltroCreate<Encomenda>(comando);
var resultEncomenda = encomendas.Where(queryEncomendas).ToList();
resultEncomenda.ForEach(e => Console.WriteLine(e.Id));



