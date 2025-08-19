using System.Text.Json.Serialization;

namespace OICT_Test.Models
{
    public class CardValidity
    {
        [JsonPropertyName("validity_start")]
        public DateTime ValidityStart { get; set; }

        [JsonPropertyName("validity_end")]
        public DateTime ValidityEnd { get; set; }
    }
}
