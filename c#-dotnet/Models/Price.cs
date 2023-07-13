using Newtonsoft.Json;
using System.Security.Cryptography.Xml;

namespace listingapi.Models
{
    public partial class Price
    {
        /// <summary>A price, expressed in euros.</summary>
        [JsonProperty("price_eur")]
        public double PriceEur { get; set; }
    }
}
