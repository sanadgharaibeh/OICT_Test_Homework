using System.Text.Json.Serialization;

namespace OICT_Test.Models
{
    public class CardStatus
    {
        [JsonPropertyName("state_id")]
        public int Id { get; set; }

        [JsonPropertyName("state_description")]
        public string Description { get; set; }
    }
}
