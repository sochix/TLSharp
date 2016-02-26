using System.Collections.Generic;

namespace TLSharp.Compiler
{
    abstract class TCompiler
    {
        internal const string vectorConstructor = "0x1cb5c415";
        
        public abstract string GetTLObjectsCode(List<TLObject> objs, int initialIndentation);
        public abstract string GetTLObjectCode(TLObject obj, int initialIndentation);

        public abstract string GetArgumentWrite(Argument arg, int initialIndentation);
        public abstract string GetArgumentRead(Argument arg, int initialIndentation);
    }
}
