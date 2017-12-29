using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;

namespace investrs.Models
{
    // To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
    //
    //    using QuickType;
    //
    //    var data = DataRs3Item.FromJson(jsonString);


        

        public partial class DataRs3Item
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("members")]
            public bool Members { get; set; }

            [JsonProperty("tradeable")]
            public bool Tradeable { get; set; }

            [JsonProperty("cosmetic")]
            public bool Cosmetic { get; set; }

            [JsonProperty("modelid")]
            public long Modelid { get; set; }

            [JsonProperty("value")]
            public long Value { get; set; }
        }

        public partial class DataRs3Item
        {
            public static Dictionary<string, DataRs3Item> FromJson(string json) => JsonConvert.DeserializeObject<Dictionary<string, DataRs3Item>>(json, Converter.Settings);
        }

        public static class SerializeRS3Item
        {
            public static string ToJson(this Dictionary<string, DataRs3Item> self) => JsonConvert.SerializeObject(self, Converter.Settings);
        }

        public class ConverterRSItem
        {
            public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
            };
        }
    

}
