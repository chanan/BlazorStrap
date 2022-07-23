using BlazorStrap.Extensions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlazorStrap.JsonConverters
{
    public class CallerNameJsonConverter : JsonConverter<CallerName>
    {
        public override CallerName Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new CallerName(reader.GetString() ?? "");
        }

        public override void Write(Utf8JsonWriter writer, CallerName callerName, JsonSerializerOptions options) =>
            writer.WriteStringValue(callerName.ToString());
    }
}