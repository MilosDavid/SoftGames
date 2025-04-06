using System;
using Newtonsoft.Json;

namespace MagicWords.DTO
{
    [Serializable]
    public class Emojy
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}