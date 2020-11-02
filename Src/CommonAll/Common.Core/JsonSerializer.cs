namespace Common.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class JsonSerializer : ISerializer
    {
        public virtual Task<string> Serialize<T>(T obj)
        {
            string jsonData = System.Text.Json.JsonSerializer.Serialize(obj);

            return Task.FromResult(jsonData);
        }

        public virtual Task<T> Decerialize<T>(string jsonData)
        {
            T obj = System.Text.Json.JsonSerializer.Deserialize<T>(jsonData);

            return Task.FromResult(obj);
        }
    }
}
