using System.Collections;
using System.Reflection;

namespace MaiaIO.DinExpressions.CLI
{
    public static class EnumUtil
    {
        public static EnumKvo<TEnum,R> EnumKeyValueMapperToKVO<TEnum, R>(TEnum enumerator)
        {
            var info = enumerator?.GetType().GetFields().FirstOrDefault();
            //return   new { Key = enumerator, Value = (R)info.GetValue(enumerator)};
            return new EnumKvo<TEnum,R>(enumerator, (R)info.GetValue(enumerator));
        }

        public static Tuple<TEnum,R> EnumKeyValueMapperToTuple<TEnum, R>(TEnum enumerator)
        {
            FieldInfo info = enumerator?.GetType().GetFields().FirstOrDefault();
            return Tuple.Create(enumerator, (R)info.GetValue(enumerator));
        }

        public static Tuple<TEnum, BitArray> EnumKeyValueMapperToBitArray<TEnum, R>(TEnum enumerator)
        {
            FieldInfo info = enumerator?.GetType().GetFields().FirstOrDefault();
            return Tuple.Create(enumerator, new BitArray((int)info?.GetValue(enumerator)));
        }

        public record EnumKvo<K,V>
        {
            public K Key { get; set; }
            public V Value { get; set; }
            public EnumKvo(K key, V value)
            {
                Key = key;
                Value = value;
            }
        }
    }
}
