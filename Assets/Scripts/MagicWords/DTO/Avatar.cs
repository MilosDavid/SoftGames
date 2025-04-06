using System;
using Newtonsoft.Json;

namespace MagicWords.DTO
{
    [Serializable]
    public class Avatar
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("url")]
        public string Url { get; set; }
        
        [JsonProperty("position")]
        public string Position 
        {
            get => _position;
            set => _position = value?.ToLower() == "right" ? "right" : "left";
        }
        
        private string _position;
    }
}