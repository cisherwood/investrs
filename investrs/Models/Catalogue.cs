using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace investrs.Models
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public partial class Catalogue
    {
        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("items")]
        public CatalogueItem[] Items { get; set; }
    }

    public partial class CatalogueItem
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
    }

    public partial class Today
    {
        [JsonProperty("trend")]
        public string Trend { get; set; }

        [JsonProperty("price")]
        public long Price { get; set; }
    }

    public partial class Current
    {
        [JsonProperty("trend")]
        public string Trend { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }
    }

    public partial class Catalogue
    {
        public static Catalogue FromJson(string json) => JsonConvert.DeserializeObject<Catalogue>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Catalogue self) => JsonConvert.SerializeObject(self, Converter.Settings);
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
