using System.Text.Json;

namespace CKO.Payments.Bank.Extensions
{
    public static class JsonExtensions
    {
        public static T Deserialize<T>(this string jsonString)
        {
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(jsonString, jsonSerializerOptions);
        }

        public static string Serialize(this object obj)
        {
            return obj.Serialize(ignoreNullValues: false);
        }

        public static string Serialize(this object obj, bool ignoreNullValues)
        {
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                IgnoreNullValues = ignoreNullValues
            };

            return JsonSerializer.Serialize(obj, jsonSerializerOptions);
        }
    }
}
