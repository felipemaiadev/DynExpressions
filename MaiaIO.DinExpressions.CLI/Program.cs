// See https://aka.ms/new-console-template for more information
using MaiaIO.DinExpressions.CLI;

Console.WriteLine("Hello, World!");


var encomandas = new List<Encomenda>() {
           new Encomenda { Id = 1, EmpresaOrigem = "MG10", EmpresaDestino = "MG45", DataCriacao = new DateTime(2024,3,1) , IsActive = true },
           new Encomenda { Id = 2, EmpresaOrigem = "MG21", EmpresaDestino = "MG32", DataCriacao = new DateTime(2024, 4, 4), IsActive = false },
           new Encomenda { Id = 3, EmpresaOrigem = "MG36", EmpresaDestino = "MG4", DataCriacao = new DateTime(2024, 1, 5), IsActive = false }
    };


var comando = new ListarEncomedaComando { EmpresaOrigem = "MG21" };

var predicate = ExpressionBuilder.FiltroCreate(comando);

var result = encomandas.Where(predicate).ToList();  

result.ForEach(e => Console.WriteLine(e.Id));