using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Schema;
namespace TeleSharp.Generator
{
    class Method
    {
        [Newtonsoft.Json.JsonProperty("id")]
        public int Id { get; set; }

        [Newtonsoft.Json.JsonProperty("method")]
        public string MethodName { get; set; }

        [Newtonsoft.Json.JsonProperty("params")]
        public List<Param> Params { get; set; }

        [Newtonsoft.Json.JsonProperty("type")]
        public string ResponseType { get; set; }

        public override bool Equals(Object obj)
        {
            return obj is Method && this == (Method)obj;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ MethodName.GetHashCode() ^ Params.GetHashCode() ^ ResponseType.GetHashCode();
        }
        public static bool operator ==(Method x, Method y)
        {
            return Enumerable.SequenceEqual(x.Params, y.Params) && x.MethodName == y.MethodName && x.ResponseType == y.ResponseType && x.Id == y.Id;
        }
        public static bool operator !=(Method x, Method y)
        {
            return !(x == y);
        }
    }
    class Param
    {
        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("type")]
        public string Type { get; set; }
    }
    class Constructor
    {
        [Newtonsoft.Json.JsonProperty("id")]
        public int Id { get; set; }

        [Newtonsoft.Json.JsonProperty("predicate")]
        public string ConstructorName { get; set; }

        [Newtonsoft.Json.JsonProperty("params")]
        public List<Param> Params { get; set; }

        [Newtonsoft.Json.JsonProperty("type")]
        public string BaseType { get; set; }

        public override bool Equals(Object obj)
        {
            return obj is Constructor && this == (Constructor)obj;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ ConstructorName.GetHashCode() ^ Params.GetHashCode() ^ BaseType.GetHashCode();
        }
        public static bool operator ==(Constructor x, Constructor y)
        {
            return Enumerable.SequenceEqual(x.Params, y.Params) && x.ConstructorName == y.ConstructorName && x.BaseType == y.BaseType && x.Id == y.Id;
        }
        public static bool operator !=(Constructor x, Constructor y)
        {
            return !(x == y);
        }
    }
    class Schema
    {
        [Newtonsoft.Json.JsonProperty("constructors")]
        public List<Constructor> Constructors { get; set; }

        [Newtonsoft.Json.JsonProperty("methods")]
        public List<Method> Methods { get; set; }

        public override bool Equals(Object obj)
        {
            return obj is Schema && this == (Schema)obj;
        }
        public override int GetHashCode()
        {
            return Constructors.GetHashCode() ^ Methods.GetHashCode();
        }
        public static bool operator ==(Schema x, Schema y)
        {
            return Enumerable.SequenceEqual(x.Methods.OrderBy(t=>t.MethodName),y.Methods.OrderBy(t => t.MethodName)) &&
                Enumerable.SequenceEqual(x.Constructors.OrderBy(t => t.ConstructorName), y.Constructors.OrderBy(t => t.ConstructorName));
        }
        public static bool operator !=(Schema x, Schema y)
        {
            return !(x == y);
        }
    }
}
