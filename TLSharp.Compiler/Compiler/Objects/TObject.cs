using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLSharp.Compiler
{
    class TLObject
    {
        public string Name;
        public string Constructor;
        public List<Argument> Args;
        public string ResultName;

        public bool IsFunction;
        public bool HasFlags => Args.Any(arg => arg.IsFlag);
        public bool HasGeneric => Args.Any(arg => arg.Generic);

        public bool GenericResult => ResultName.Equals(Args.First(arg => arg.Generic).Type);

        public TLObject(string name, string constructor, List<Argument> args, string resultName, bool isFunction)
        {
            Name = name; // ensure that the constructor length is 8 (i.e. instead of #123456 -> #01234567, stylistic purposes only)
            Constructor = constructor.Length == 8 ? constructor : new string('0', 8 - constructor.Length) + constructor;
            Args = args;
            ResultName = resultName;

            IsFunction = isFunction;
        }

        public override string ToString()
        {
            return $"{Name}#{Constructor}{(HasGeneric ? getGenericTypes() : "")}{(HasFlags ? " flags:#" : "")}{(Args.Count > 0 ? " " + string.Join(" ", Args) : "")} = {ResultName};";
        }

        public IEnumerable<Argument> GetFlags() => Args.Where(arg => arg.IsFlag);
        public IEnumerable<Argument> GetGeneric() => Args.Where(arg => arg.Generic);

        string getGenericTypes()
        {
            var sb = new StringBuilder();
            foreach (var arg in Args.Where(arg => arg.Generic))
            {
                sb.Append(" {");
                sb.Append(arg.Type);
                sb.Append(':');
                sb.Append("Type");
                sb.Append('}');
            }
            return sb.ToString();
        }
    }
}
