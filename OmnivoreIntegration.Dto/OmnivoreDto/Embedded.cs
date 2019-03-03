using Newtonsoft.Json;

namespace OmnivoreIntegration.Dto
{
    public class Embedded
    {
        [JsonProperty("tender_type")]
        public TenderType TenderType { get; set; }
        [JsonProperty("ticket")]
        public Ticket Ticket { get; set; }
    }
}
