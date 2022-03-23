using System.Text.Json.Serialization;

namespace VY.RatesApi.Business.Contracts.Domain
{
    public class Rate
    {
        [JsonPropertyName("from")]
        public string From { get; set; }
        [JsonPropertyName("to")]
        public string To { get; set; }
        [JsonPropertyName("rate")]
        public string Ratio { get; set; }
        [JsonIgnore]
        public bool IsVisited { get; set; }
    }
}
