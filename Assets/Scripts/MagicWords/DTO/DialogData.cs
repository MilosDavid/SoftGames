using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MagicWords.DTO
{
    [Serializable]
    public class DialogData
    {
        [JsonProperty("dialogue")]
        public List<Dialogue> Dialogue { get; set; }
        [JsonProperty("emojies")]
        public List<Emojy> Emojies { get; set; }
        [JsonProperty("avatars")]
        public List<Avatar> Avatars { get; set; }
    }
}