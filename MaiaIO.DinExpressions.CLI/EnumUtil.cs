using MaiaIO.DinExpressions.CLI.Enums;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;

namespace MaiaIO.DinExpressions.CLI
{
    public static class EnumUtil
    {

        public static dynamic EnumKeyValueMapper<TEnum>(TEnum enumerator)
        {
            FieldInfo[] infos = enumerator.GetType().GetFields();
           
            foreach(var info in infos)
            {
                Console.WriteLine(info.GetValue(enumerator));
                var obj = (info) switch
                {
                    { DeclaringType: var tipo } when tipo == typeof(SimNaoEnum)=> new { Key = info.Name, Value = (int)info.GetValue(enumerator) },
                    { DeclaringType: var tipo } when tipo == typeof(StatusEnum) => new { Key = info.Name, Value = (int)info.GetValue(enumerator) },
                    { DeclaringType: var tipo } when tipo == typeof(StatusPedidoEnum) => new { Key = info.Name, Value = (int)info.GetValue(enumerator) },
                    _ => throw new NotImplementedException()
                };

            };



        FieldInfo[] fields = typeof(TEnum).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var field in fields)
            {
                Console.WriteLine($"Name: {field.Name}, Value: {(int)field.GetValue(null)}");

                var value = field switch
                {
                    { FieldType : var tipo } when tipo == typeof(SimNaoEnum) => new { Key = field.Name, Value = (int)field.GetValue(null) }
                };
            }

            


            return new { Key = "AAAAA" , Value = 1 };
        }
    }

}
