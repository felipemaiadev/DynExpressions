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

        public static Expression DateTimeExpressionResolver(PropertyInfo info, Expression expression, ParameterExpression parameter, R filtro)
        {

            string attributeData = info.CustomAttributes
                                   .Where(x => x.ConstructorArguments.Count > 0 )
                                   .Select(x => x.ConstructorArguments)
                                   .FirstOrDefault()
                                   .Select(x => x.Value)
                                   .FirstOrDefault() as string;
                             

            Expression Select(string attributeData) => attributeData switch
            {
                 "Begin"  => DateTimeExpressionBeginResolver(info, expression, parameter, filtro),
                 "End" => DateTimeExpressionEndResolver(info, expression, parameter, filtro),
                  _ => NotMapperdExpressionResolver(info, expression, parameter, filtro)
            };

           return Select(attributeData);
        }
        public static Expression DateTimeExpressionBeginResolver(PropertyInfo info, Expression expression, ParameterExpression parameter, R filtro)
        {

            string propertyName = info.CustomAttributes
                                   .Where(x => x.ConstructorArguments.Count > 0)
                                   .Select(x => x.ConstructorArguments)
                                   .FirstOrDefault()
                                   .Select(x => x.Value)
                                   .Skip(1)
                                   .FirstOrDefault() as string;

            DateTime value = (DateTime)info.GetValue(filtro);

            if (DateTime.MinValue == value) return expression;

            MemberExpression filterParameter = Expression.Property(parameter, propertyName);
            ConstantExpression constParamerter = Expression.Constant(value);
            BinaryExpression operation = Expression.GreaterThanOrEqual(filterParameter, constParamerter);

            expression = expression == null ? operation : Expression.And(expression, operation);

            return expression;
        }

        public static Expression DateTimeExpressionEndResolver(PropertyInfo info, Expression expression, ParameterExpression parameter, R filtro)
        {

            string propertyName = info.CustomAttributes
                                  .Where(x => x.ConstructorArguments.Count > 0)
                                  .Select(x => x.ConstructorArguments)
                                  .FirstOrDefault()
                                  .Select(x => x.Value)
                                  .Skip(1)
                                  .FirstOrDefault() as string;

            DateTime value = (DateTime)info.GetValue(filtro);

            if (DateTime.MinValue == value) return expression;

            MemberExpression filterParameter = Expression.Property(parameter, propertyName);
            ConstantExpression constParamerter = Expression.Constant(value);
            BinaryExpression operation = Expression.LessThanOrEqual(filterParameter, constParamerter);

            expression = expression == null ? operation : Expression.And(expression, operation);

            return expression;
        }

        public static Expression DateTimeBaseExpressionEndResolver(PropertyInfo info, Expression expression, ParameterExpression parameter, R filtro)
        {
            DateTime value = (DateTime)info.GetValue(filtro);

            if (DateTime.MinValue == value) return expression;

            MemberExpression filterParameter = Expression.Property(parameter, info.Name);
            ConstantExpression constParamerter = Expression.Constant(value);
            BinaryExpression operation = Expression.Equal(filterParameter, constParamerter);

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

        public static Expression ArrayExpressionResolver(PropertyInfo info, Expression expression, ParameterExpression parameter, R filtro)
        {
            var value = info.GetValue(filtro);

            if (value is  null) return expression;

            var parameterProducts = Expression.Property(parameter, "Produtos");
            var filterParameter = Expression.Parameter(typeof(Produto), "Produto");

            var containMethod = typeof(List<long>).GetMethod("Contains", new[] { typeof(long) });
            var idProperty = Expression.Property(filterParameter, "Id");
            var containsCall = Expression.Call(Expression.Constant(value), containMethod, idProperty);

            var anyMethod = typeof(Enumerable).GetMethods()
                                                .Where(m => m.Name == "Any" && m.GetParameters().Length == 2)
                                                .Single()
                                                .MakeGenericMethod(typeof(Produto));

            var operation = Expression.Call(null, anyMethod, parameterProducts, Expression.Lambda(containsCall, filterParameter));


            expression = expression == null ? operation : Expression.And(expression, operation);

            return expression;
        }

        public static Expression NotMapperdExpressionResolver(PropertyInfo info, Expression expression, ParameterExpression parameter, R filtro)
        {
            return expression;
        }

        public static Func<T,bool> DefaultExpressionResolver(Expression expression, ParameterExpression parameter)
        {
            Func<T, bool> query = null;
            ConstantExpression constant = Expression.Constant(true);

            if (expression is null)
            {
                expression = Expression.Equal(constant, parameter);
                query = Expression.Lambda<Func<T,bool>>(expression, parameter).Compile();
                return  query;
            }

            query = Expression.Lambda<Func<T, bool>>(expression, parameter).Compile();
            return query;
        }




        public static Expression ExpressionSelector(PropertyInfo info, Expression expression, ParameterExpression parameter, R filtro) => info switch
        {

            { PropertyType: var type, Name: var name } when type == typeof(string)  => StringExpressionResolver(info, expression, parameter, filtro),
            { PropertyType: var type, Name: var name } when type == typeof(long) => LongExpressionResolver(info, expression, parameter, filtro),
            { PropertyType: var type, CustomAttributes: var attb } when type == typeof(DateTime) && attb.Count() > 0 => DateTimeExpressionResolver(info, expression, parameter, filtro),
            { PropertyType: var type, CustomAttributes: var attb } when type == typeof(DateTime) && attb.Count() == 0 => DateTimeBaseExpressionEndResolver(info, expression, parameter, filtro),
            { PropertyType: var type } when type == typeof(List<long>) => ArrayExpressionResolver(info, expression, parameter, filtro),
            _  => NotMapperdExpressionResolver(info, expression, parameter, filtro)
        };






    }
}
