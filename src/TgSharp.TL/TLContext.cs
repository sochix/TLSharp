using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TgSharp.TL
{
    public static class TLContext
    {
        private static Dictionary<int, Type> Types;

        static TLContext()
        {
            Types = new Dictionary<int, Type>();
            Types = (from t in Assembly.GetExecutingAssembly().GetTypes()
                     where t.IsClass && t.Namespace.StartsWith(typeof(TLContext).Namespace)
                     where t.IsSubclassOf(typeof(TLObject))
                     where t.GetCustomAttribute(typeof(TLObjectAttribute)) != null
                     select t).ToDictionary(x => ((TLObjectAttribute)x.GetCustomAttribute(typeof(TLObjectAttribute))).Constructor, x => x);


            var vectorTypeId = 481674261;
            var genericVectorType = typeof (TLVector<>);

            Type type;
            if (Types.TryGetValue(vectorTypeId, out type)) {
                if (type != genericVectorType && type != typeof(TLVector)) {
                    throw new InvalidOperationException ($"Type {vectorTypeId} should have been a TLVector type but was {type}");
                }
            } else {
                Types [vectorTypeId] = genericVectorType;
            }
        }

        public static Type getType(int Constructor)
        {
            return Types[Constructor];
        }
    }
}
