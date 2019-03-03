using System.Collections.Generic;

using Newtonsoft.Json;

namespace OmnivoreIntegration.Dto
{
    public class ErrorSlug
    {
        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("fields")]
        public List<string> Fields { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("_embedded")]
        public Embedded Embedded { get;set;}
    }
}