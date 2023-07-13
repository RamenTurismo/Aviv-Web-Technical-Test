using Newtonsoft.Json;
using System;

namespace listingapi.Models
{
    public partial class PriceReadOnly : Price
    {
        [JsonProperty("created_date")]
        public DateTimeOffset CreatedDate { get; set; }
    }
}
