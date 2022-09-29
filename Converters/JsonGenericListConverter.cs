using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Sprocket.Converters;

/// <summary>
/// Converts Singleton objects to List on Json Deserialization
/// </summary>
/// <typeparam name="T"></typeparam>
public class GenericListConverter<T> : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(List<T>);
    }

    public override object ReadJson(JsonReader reader, Type type, object obj, JsonSerializer jsonSerializer)
    {
        JToken token = JToken.Load(reader);
        return token.Type == JTokenType.Array ? token.ToObject<List<T>>() : new List<T> { token.ToObject<T>() };
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException("WriteJson() is not implemented");
    }
}