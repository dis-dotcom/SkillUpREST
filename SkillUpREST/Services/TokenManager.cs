using System;
using System.Linq;
using System.Collections.Generic;

namespace SkillUpREST.Services
{
    public static class TokenManager
    {
        private static List<Guid> tokens = new();

        public static bool IsValid(Guid token) => tokens.Any(t => t == token);

        public static void RemoveToken(Guid token)
        {
            tokens.Remove(token);
        }

        public static T GenerateToken<T>()
        {
            var token = Guid.NewGuid();
            var targetType = typeof(T);

            Dictionary<Type, Func<Guid, object>> mapConvert = new()
            {
                { typeof(Guid), guid => token },
                { typeof(string), guid => token.ToString() }
            };

            if (mapConvert.ContainsKey(targetType))
            {
                T result = (T)mapConvert[targetType](token);

                tokens.Add(token);

                return result;
            }

            throw new ArgumentException();
        }
    }
}
