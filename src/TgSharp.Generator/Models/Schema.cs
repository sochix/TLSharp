using System.Collections.Generic;
using Newtonsoft.Json;

namespace TgSharp.Generator.Models
{
    internal class Schema
    {
        [JsonProperty("constructors")] 
        public List<Constructor> Constructors { get; set; }

        [JsonProperty("methods")] 
        public List<Function> Methods { get; set; }
    }
}