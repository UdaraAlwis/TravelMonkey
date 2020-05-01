using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelMonkey.Models.CognitiveServices
{
    public partial class TranslationStatsResult
    {
        [JsonProperty("translation")]
        public Dictionary<string, Dictionary> Translation { get; set; }

        [JsonProperty("transliteration")]
        public Dictionary<string, Transliteration> Transliteration { get; set; }

        [JsonProperty("dictionary")]
        public Dictionary<string, Dictionary> Dictionary { get; set; }
    }

    public partial class Dictionary
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nativeName")]
        public string NativeName { get; set; }

        [JsonProperty("dir")]
        public Dir Dir { get; set; }

        [JsonProperty("translations", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary[] Translations { get; set; }

        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; }

        [JsonProperty("toScripts", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary[] ToScripts { get; set; }
    }

    public partial class Transliteration
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nativeName")]
        public string NativeName { get; set; }

        [JsonProperty("scripts")]
        public Dictionary[] Scripts { get; set; }
    }

    public enum Dir { Ltr, Rtl };

    public class AvailableLanguage
    {
        public string Key { get; set; }

        public string Name { get; set; }

        public string NativeName { get; set; }

        public bool IsSelected { get; set; }
    }
}
