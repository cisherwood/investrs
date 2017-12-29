using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace investrs.Models
{
    public partial class DataItemPriceHistory
    {
        [JsonProperty("daily")]
        public Dictionary<string, long> Daily { get; set; }

        [JsonProperty("average")]
        public Dictionary<string, long> Average { get; set; }
    }

    public partial class DataItemPriceHistory
    {
        public static DataItemPriceHistory FromJson(string json) => JsonConvert.DeserializeObject<DataItemPriceHistory>(json, Converter.Settings);
    }

    public static class SerializeItemPriceHistory
    {
        public static string ToJson(this DataItemPriceHistory self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    public class ConverterItemPriceHistory
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}
