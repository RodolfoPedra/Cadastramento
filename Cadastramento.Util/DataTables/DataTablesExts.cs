using System;
using Cadastramento.Util.DataTables.DataTables.AspNet.Core;
using Newtonsoft.Json.Linq;

namespace Cadastramento.Util.DataTables
{
    public static class DataTablesExts
    {
        public static T Get<T>(this IDataTablesRequest request, string key)
        {
            var obj = request.AdditionalParameters[key];
            var type = typeof(T);
            if (type == typeof(DateTime) || type == typeof(DateTime?) && obj != null)
            {
                var splited = obj.ToString().Split('/');
                obj = splited[2] + "-" + splited[1] + "-" + splited[0];
            }

            return Convert<T>(obj);
        }

        public static bool TryGet<T>(this IDataTablesRequest request, string key, out T value)
        {
            if (request.AdditionalParameters.TryGetValue(key, out object obj))
            {
                value = Convert<T>(obj);
                return true;
            }

            value = default(T);
            return false;
        }

        private static readonly JToken JNull = JToken.Parse("null");
        private static T Convert<T>(object obj)
        {
            T AsNull() => JNull.ToObject<T>();

            if (obj == null)
                return AsNull();

            if (obj is T tobj)
                return tobj;

            if (obj is JToken jt)
                return jt.ToObject<T>();

            try
            {
                return JToken.FromObject(obj).ToObject<T>();
            }
            catch (FormatException) when (obj is string s && string.IsNullOrWhiteSpace(s))
            {
                return AsNull();
            }
        }
    }
}