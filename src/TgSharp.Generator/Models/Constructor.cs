using System.Collections.Generic;

using Newtonsoft.Json;

namespace TgSharp.Generator.Models
{
    internal class Constructor
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("predicate")] 
        public string Predicate { get; set; }

        [JsonProperty("params")] 
        public List<Param> Params { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}