using System.Collections.Generic;

using Newtonsoft.Json;

namespace TgSharp.Generator.Models
{
    //called 'Function' because C# compiler doesn't let a property name == class
    internal class Function
    {
        [JsonProperty("id")] 
        public int Id { get; set; }

        [JsonProperty("method")] 
        public string Method { get; set; }

        [JsonProperty("params")] 
        public List<Param> Params { get; set; }

        [JsonProperty("type")] 
        public string Type { get; set; }
    }
}