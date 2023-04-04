using System.Text.Json.Serialization;

namespace Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RpgClass
    {
        Melee = 1,
        Mage = 2,
        Ranger = 3
    }
}