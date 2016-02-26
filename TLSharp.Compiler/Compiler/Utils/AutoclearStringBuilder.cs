using System.Text;

namespace TLSharp.Compiler
{
    class AutoclearStringBuilder
    {
        StringBuilder sb = new StringBuilder();

        public int Length => sb.Length;
        public bool NotEmpty => Length > 0;

        public void Append(char c) { sb.Append(c); }
        public void Clear() { sb.Clear(); }

        public override string ToString() { return this; }
        public static implicit operator string(AutoclearStringBuilder asb)
        {
            var result = asb.Peek();
            asb.sb.Clear();
            return result;
        }

        public string Peek() { return sb.ToString().Trim(); }
    }
}
