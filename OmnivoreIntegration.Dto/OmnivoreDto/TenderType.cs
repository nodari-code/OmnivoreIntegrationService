using Newtonsoft.Json;

namespace OmnivoreIntegration.Dto
{
    public class TenderType
    {
        /// <summary>
        /// Whether or not this tender is configured to accept tips.
        /// </summary>
        [JsonProperty("allows_tips")]
        public bool AllowsTips { get; set; }

        /// <summary>
        /// The Tender Type ID. Unique for each Location.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Tender type name as a source of payment.
        /// "3rd_party" for Third Party payments in virtual POS
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The Tender Type ID as displayed to POS admins. May not be unique.
        /// </summary>
        [JsonProperty("pos_id")]
        public string PosId { get; set; }
    }
}
