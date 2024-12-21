using System.Linq.Expressions;
using System.Reflection;

namespace MaiaIO.DinExpressions.CLI
{
    public class GenericExpressionBuilder<R, T> where T : class where R : class
    {

        public static Func<T, bool> FiltroCreate<T>(R filtro)
        {
            Expression expression = null;
            ParameterExpression parameter = Expression.Parameter(typeof(T), typeof(T).Name);

            PropertyInfo[] infos = filtro.GetType().GetProperties();

            foreach (PropertyInfo info in infos)
            {
               expression =  ExpressionSelector(info, expression ,parameter, filtro);
            }


            Func<T, bool> predicado = Expression.Lambda<Func<T, bool>>(expression, parameter).Compile();

            return predicado;

        }

        public static Expression StringExpressionResolver(PropertyInfo info, Expression expression, ParameterExpression parameter, R filtro)
        {
            string value = (string)info.GetValue(filtro);

            if (string.IsNullOrEmpty(value)) return expression;
            
            MemberExpression filterParameter = Expression.Property(parameter, info.Name);
            ConstantExpression constParamerter = Expression.Constant(value);
            MethodCallExpression operation = Expression.Call(filterParameter, "Contains", Type.EmptyTypes, constParamerter);

            expression = expression == null ? operation : Expression.And(expression, operation);

            return expression;
        }


        public static Expression LongExpressionResolver(PropertyInfo info, Expression expression, ParameterExpression parameter, R filtro)
        {
            long value = (long)info.GetValue(filtro);

            if (value == 0) return expression;

            MemberExpression filterParameter = Expression.Property(parameter, info.Name);
            ConstantExpression constParamerter = Expression.Constant(value);
            BinaryExpression operation = Expression.Equal(filterParameter, constParamerter);

            expression = expression == null ? operation : Expression.And(expression, operation);

            return expression;
        }

        public static Expression NotMapperdExpressionResolver(PropertyInfo info, Expression expression, ParameterExpression parameter, R filtro)
        {
            return expression;
        }

        //public static Func<T,bool> DefaultExpressionResolver(Expression expression)
        //{
        //    if (expression is null) 
        //}


        public static Expression ExpressionSelector(PropertyInfo info, Expression expression, ParameterExpression parameter, R filtro) => info switch
        {

            { PropertyType: var type, Name: var name } when type == typeof(string) => StringExpressionResolver(info, expression, parameter, filtro),
            { PropertyType: var type, Name: var name } when type == typeof(long) => LongExpressionResolver(info, expression, parameter, filtro),
             _  => NotMapperdExpressionResolver(info, expression, parameter, filtro)
        };






    }
}
