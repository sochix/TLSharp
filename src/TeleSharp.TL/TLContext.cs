using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using TeleSharp.TL;
namespace TeleSharp.TL
{
    public static class TLContext
    {
        private static Dictionary<int, Type> Types;

        static TLContext()
        {
            Types = new Dictionary<int, Type>();
            Types = (from t in Assembly.GetExecutingAssembly().GetTypes()
                     where t.IsClass && t.Namespace.StartsWith("TeleSharp.TL")
                     where t.IsSubclassOf(typeof(TLObject))
                     where t.GetCustomAttribute(typeof(TLObjectAttribute)) != null
                     select t).ToDictionary(x => ((TLObjectAttribute)x.GetCustomAttribute(typeof(TLObjectAttribute))).Constructor, x => x);
            Types.Add(481674261, typeof(TLVector<>));
        }

        public static Type getType(int Constructor)
        {
            return Types[Constructor];
        }
    }
}
