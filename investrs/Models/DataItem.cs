using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace investrs.Models
{
    public partial class DataItem
    {
        [JsonProperty("item")]
        public DataItemResult Item { get; set; }
    }

    public partial class DataItemResult
    {
        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("icon_large")]
        public string IconLarge { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("typeIcon")]
        public string TypeIcon { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("current")]
        public Current Current { get; set; }

        [JsonProperty("today")]
        public Today Today { get; set; }

        [JsonProperty("members")]
        public string Members { get; set; }

        [JsonProperty("day30")]
        public Day Day30 { get; set; }

        [JsonProperty("day90")]
        public Day Day90 { get; set; }

        [JsonProperty("day180")]
        public Day Day180 { get; set; }
    }

    public partial class Today
    {
        [JsonProperty("trend")]
        public string Trend { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }
    }

    public partial class Day
    {
        [JsonProperty("trend")]
        public string Trend { get; set; }

        [JsonProperty("change")]
        public string Change { get; set; }
    }

    public partial class Current
    {
        [JsonProperty("trend")]
        public string Trend { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }
    }

    public partial class DataItem
    {
        public static DataItem FromJson(string json) => JsonConvert.DeserializeObject<DataItem>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this DataItem self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    public class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}
