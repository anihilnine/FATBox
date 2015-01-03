using System.Collections.Generic;

namespace FATBox.Util.Extensions
{
    public static class EnumerableEx
    {
        public static T2 TryGetValue<T1, T2>(this Dictionary<T1, T2> dic, T1 key)
        {
            T2 t2;
            if (dic.TryGetValue(key, out t2))
            {
                return t2;
            }
            return default(T2);
        }
    }
}