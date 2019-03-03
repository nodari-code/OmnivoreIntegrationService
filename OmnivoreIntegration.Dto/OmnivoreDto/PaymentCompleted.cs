using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace OmnivoreIntegration.Dto
{
    public class PaymentCompleted 
    {
        [JsonProperty("amount")]
        public int Amount { get; set; }
        [JsonProperty("change")]
        public int Change { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("tip")]
        public int Tip { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("_embedded")]
        public Embedded Embedded { get; set; }

        public bool Successful { get; set; }

        [JsonProperty("errors")]
        public List<ErrorSlug> Errors { get; set; }

        /// <summary>
        /// Returns combined message from all Omnivore error slugs and payment data
        /// </summary>
        /// <returns>Combined error message</returns>
        public string ToErrorMessage()
        {
            StringBuilder sb = new StringBuilder();
            if (Errors != null && Errors.Count > 0)
            {
                foreach (ErrorSlug error in Errors)
                {
                    if (!string.IsNullOrEmpty(error.Error))
                    {
                        sb.Append(error.Error);
                        sb.Append(Environment.NewLine);
                    }
                    if (!string.IsNullOrEmpty(error.Description))
                    {
                        sb.Append(error.Description);
                        sb.Append(Environment.NewLine);
                    }
                }

                if (Embedded != null && Embedded.TenderType != null &&
                    !string.IsNullOrEmpty(Embedded.TenderType.Id))
                {
                    sb.Append(string.Format("Tender Type Id: {0}{1}", Embedded.TenderType.Id, Environment.NewLine));
                }
                if (Embedded != null && Embedded.TenderType != null &&
                    !string.IsNullOrEmpty(Embedded.TenderType.Name))
                {
                    sb.Append(string.Format("Tender Type Name: {0}{1}", Embedded.TenderType.Name, Environment.NewLine));
                }
                if (Embedded != null && Embedded.TenderType != null &&
                    !string.IsNullOrEmpty(Embedded.TenderType.PosId))
                {
                    sb.Append(string.Format("Tender Type Name: {0}{1}", Embedded.TenderType.PosId, Environment.NewLine));
                }
            }
            return sb.ToString();
        }
    }
}
