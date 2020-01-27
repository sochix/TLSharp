using Newtonsoft.Json;

namespace TeleSharp.Generator.Models
{
    internal class TlParam
    {
        [JsonProperty("name")] 
        public string Name { get; set; }

        [JsonProperty("type")] 
        public string Type { get; set; }
    }
}