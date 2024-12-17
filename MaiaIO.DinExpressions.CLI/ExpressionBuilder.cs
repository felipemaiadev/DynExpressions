using System;
using System.Linq.Expressions;

namespace MaiaIO.DinExpressions.CLI
{
    public static class ExpressionBuilder
    {
        public static Func<Encomenda, bool> FiltroCreate(ListarEncomedaComando comando)
        {


            Expression queryFilter = null;
            var pEmpresaOrigem = Expression.Parameter(typeof(Encomenda), "encomeda");


            var filters = comando.GetType().GetProperties();

            foreach (var filter in filters)
            {

                Console.WriteLine(filter.Name);

                var propEmpresaOrigem = Expression.Property(pEmpresaOrigem, "EmpresaOrigem");
                var constEmpresaOrigem = Expression.Constant(comando.EmpresaOrigem);
                var oprCompare = Expression.Call(propEmpresaOrigem, "Contains", Type.EmptyTypes, constEmpresaOrigem);

                queryFilter = queryFilter == null ? oprCompare : Expression.And(queryFilter, oprCompare);

                break;
            }


         

            var expression = Expression.Lambda<Func<Encomenda, bool>>(queryFilter, pEmpresaOrigem);

            Func<Encomenda, bool> predicate = expression.Compile() ;
            return predicate;
        }
    }
}
