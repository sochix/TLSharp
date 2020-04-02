using System.Collections.Generic;
using Newtonsoft.Json;

namespace TeleSharp.Generator.Models
{
    internal class TlSchema
    {
        [JsonProperty("constructors")] 
        public List<TlConstructor> Constructors { get; set; }

        [JsonProperty("methods")] 
        public List<TlMethod> Methods { get; set; }
    }
}