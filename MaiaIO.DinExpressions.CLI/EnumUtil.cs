namespace MaiaIO.DinExpressions.CLI
{
    public static class EnumUtil
    {
        public static dynamic EnumKeyValueMapperToKVO<TEnum, R>(TEnum enumerator)
        {
            var info = enumerator?.GetType().GetFields().FirstOrDefault();
            return   new { Key = enumerator, Value = (R)info.GetValue(enumerator)};
        }

    public static dynamic EnumKeyValueMapperToTuple<TEnum, R>(TEnum enumerator)
    {
        var info = enumerator?.GetType().GetFields().FirstOrDefault();
        return Tuple.Create(enumerator, (R)info.GetValue(enumerator));
    }

    }
}
