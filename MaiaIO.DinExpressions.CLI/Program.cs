// See https://aka.ms/new-console-template for more information
using MaiaIO.DinExpressions.CLI;

Console.WriteLine("<== TESTE pedidos ====>");


var pdd01 = new List<Produto>  { new Produto { Id=100, Price=20, ProdName="Bleach Edicao #102"  } ,
                                 new Produto { Id=120, Price=26 , ProdName="Bersek Edicao #09"  },
                                 new Produto { Id=202, Price=25 , ProdName="Yuyu Hakusho Edicao #22" }
                               };

var pdd02 = new List<Produto>  { new Produto { Id=310, Price=20, ProdName="Bleach Edicao #102"  } ,
                                 new Produto { Id=120, Price=26 , ProdName="Bersek Edicao #09"  },
                                 new Produto { Id=102, Price=25 , ProdName="Sailor Mooon  #02" }
                               };

var pdd03 = new List<Produto>  { new Produto { Id=100, Price=20, ProdName="Bleach Edicao #102"  } ,
                                 new Produto { Id=120, Price=26 , ProdName="Bersek Edicao #09"  },
                                 new Produto { Id=102, Price=25 , ProdName="Sailor Mooon  #02" }
                               };

var pedidos = new List<Pedido>() {
           new Pedido { Id = 1, NomeCliente = "Channing Matthew Tatum", DataCompra = new DateTime(2024,3,1) , IsActive = true, Produtos = pdd01 },
           new Pedido { Id = 2, NomeCliente =  "Joseph Michael Manganiello", DataCompra = new DateTime(2024, 4, 4), IsActive = false, Produtos = pdd01 },
           new Pedido { Id = 3, NomeCliente = "Matthew Staton Bomer", DataCompra = new DateTime(2024, 1, 5), IsActive = false, Produtos = pdd02 },
           new Pedido { Id = 4, NomeCliente = "Alexander Richard Pettyfer", DataCompra = new DateTime(2024, 1, 5), IsActive = false, Produtos = pdd03 }
    };

var comandoPedido = new ListarPedidoComando { ProdutosId = new[] {102} };

Func<Pedido, bool> queryPedidos = GenericExpressionBuilder<ListarPedidoComando, Pedido>.FiltroCreate<Pedido>(comandoPedido);
var resultPedido = pedidos.Where(queryPedidos).ToList();
resultPedido.ForEach(e => Console.WriteLine(e.Id));



Console.WriteLine("<== TESTE Encomendas ====>");

var encomendas = new List<Encomenda>() {
           new Encomenda { Id = 1, EmpresaOrigem = "XPTO10", EmpresaDestino = "XPTO15", DataCriacao = new DateTime(2024,3,1), PrevisaoChegada = new DateTime(2024,3,1) , IsActive = true },
           new Encomenda { Id = 2, EmpresaOrigem = "PGTO21", EmpresaDestino = "XPTO12", DataCriacao = new DateTime(2024, 4, 4), PrevisaoChegada = new DateTime(2024, 4, 4) , IsActive = false },
           new Encomenda { Id = 3, EmpresaOrigem = "WHTO36", EmpresaDestino = "XPTO14", DataCriacao = new DateTime(2024, 1, 5), PrevisaoChegada = new DateTime(2024, 1, 5), IsActive = false },
           new Encomenda { Id = 4, EmpresaOrigem = "XPTO90", EmpresaDestino = "XPTO28", DataCriacao = new DateTime(2024,6,1), PrevisaoChegada =  new DateTime(2024,6,1) , IsActive = true },
    };


var comando = new ListarEncomedaComando { PrevisaoChegadaInicio = new DateTime(2024, 3, 5) , PrevisaoChegadaFim = new DateTime(2024, 5, 31) };

Func<Encomenda, bool> queryEncomendas = GenericExpressionBuilder<ListarEncomedaComando, Encomenda>.FiltroCreate<Encomenda>(comando);
var resultEncomenda = encomendas.Where(queryEncomendas).ToList();
resultEncomenda.ForEach(e => Console.WriteLine(e.Id));



