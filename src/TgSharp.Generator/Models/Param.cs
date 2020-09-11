using Newtonsoft.Json;

namespace TgSharp.Generator.Models
{
    internal class Param
    {
        [JsonProperty("name")] 
        public string Name { get; set; }

        [JsonProperty("type")] 
        public string Type { get; set; }
    }
}