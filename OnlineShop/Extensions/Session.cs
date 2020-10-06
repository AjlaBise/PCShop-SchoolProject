using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Extensions
{
    public static class SessionExtension
    {
        public static T GetObject<T>(this ISession session, string key) where T : class
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
        
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            var stringifiedObject = JsonConvert.SerializeObject(value);
            session.SetString(key, stringifiedObject);
        }

        public static void RemoveObject(this ISession session, string key)
        {
            session.Remove(key);
        }
    }
}
