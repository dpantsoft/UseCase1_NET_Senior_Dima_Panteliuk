using System.Text.Json;
using System.Text.Json.Serialization;
using UseCase1.Models;

namespace UseCase1.Services
{
    public class CountryConverter : JsonConverter<Country>
    {
        public override Country Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var root = doc.RootElement;
                var commonName = root.GetProperty("name").GetProperty("common").GetString();

                return new Country { Name = commonName };
            }
        }

        public override void Write(Utf8JsonWriter writer, Country value, JsonSerializerOptions options)
        {
            throw new NotImplementedException(); // Implement if you need to serialize back to JSON.
        }

    }
}
