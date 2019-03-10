using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IdealistaTest.Domain.Entities
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Typology
    {
        CHALET,
        FLAT,
        GARAGE
    }
}