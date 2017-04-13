using System.Collections.Generic;

namespace TeleSharp.Generator
{
    internal class Method
    {
        public int id { get; set; }
        public string method { get; set; }

        [Newtonsoft.Json.JsonProperty("params")]
        public List<Param> Params { get; set; }

        public string type { get; set; }
    }

    internal class Param
    {
        public string name { get; set; }
        public string type { get; set; }
    }

    internal class Constructor
    {
        public int id { get; set; }
        public string predicate { get; set; }

        [Newtonsoft.Json.JsonProperty("params")]
        public List<Param> Params { get; set; }

        public string type { get; set; }
    }

    internal class Schema
    {
        public List<Constructor> constructors { get; set; }
        public List<Method> methods { get; set; }
    }
}