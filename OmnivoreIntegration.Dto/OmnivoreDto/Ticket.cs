using Newtonsoft.Json;

namespace OmnivoreIntegration.Dto
{
    public class Ticket
    {
        [JsonProperty("auto_send")]
        public bool AutoSend { get; set; }

        [JsonProperty("closed_at")]
        public long ClosedAt { get; set; }

        [JsonProperty("guest_count")]
        public int GuestCount { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("open")]
        public bool Open { get; set; }

        [JsonProperty("opened_at")]
        public long OpenedAt { get; set; }

        [JsonProperty("ticket_number")]
        public string TicketNumber { get; set; }

        [JsonProperty("totals")]
        public Totals Totals { get; set; }

        [JsonProperty("void")]
        public bool Void { get; set; }
    }
}
