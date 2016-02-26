using System;
using System.Text;

namespace TLSharp.Compiler
{
    struct Argument
    {
        public string Name;
        public string Type;
        public bool Generic;
        public int FlagIndex;

        public bool IsFlag => FlagIndex > -1;

        public bool IsVector => Type.IndexOf("vector<", StringComparison.OrdinalIgnoreCase) > -1;
        public string InnerVectorType =>
            Type.Substring(Type.IndexOf('<') + 1, Type.Length - Type.IndexOf('<') - 2);

        public Argument(string name, string type)
        {
            Name = name;
            Type = type;
            Generic = false;
            FlagIndex = -1;
        }

        public Argument(string name, string type, bool generic, int flagIndex)
        {
            Name = name;
            Type = type;
            Generic = generic;
            FlagIndex = flagIndex;
        }

        public override string ToString()
        {
            return IsFlag ?
                $"{Name}:flags.{FlagIndex}?{Type}" :
                $"{Name}:{(Generic ? "!" : "")}{Type}";
        }
    }
}
