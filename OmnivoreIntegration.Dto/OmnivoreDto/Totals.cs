using Newtonsoft.Json;

namespace OmnivoreIntegration.Dto
{
    public class Totals
    {
        [JsonProperty("discounts")]
        public int Discounts { get; set; }

        [JsonProperty("due")]
        public int Due { get; set; }

        [JsonProperty("items")]
        public int Items { get; set; }

        [JsonProperty("other_charges")]
        public int OtherCharges { get; set; }

        [JsonProperty("paid")]
        public int Paid { get; set; }

        [JsonProperty("service_charges")]
        public int ServiceCharges { get; set; }

        [JsonProperty("sub_total")]
        public int SubTotal { get; set; }

        [JsonProperty("tax")]
        public int Tax { get; set; }

        [JsonProperty("tips")]
        public int Tips { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }
}
