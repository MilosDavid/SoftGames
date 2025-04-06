using System;
using Newtonsoft.Json;

namespace MagicWords.DTO
{
    [Serializable]
    public class Dialogue
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}