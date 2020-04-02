using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TeleSharp.TL;
namespace TeleSharp.TL
{
    public class ObjectUtils
    {
        public static object DeserializeObject(BinaryReader reader)
        {
            int Constructor = reader.ReadInt32();
            object obj;
            Type t = null;
            try
            {
                t = TLContext.getType(Constructor);
                obj = Activator.CreateInstance(t);
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("Constructor Invalid Or Context.Init Not Called !", ex);
            }
            if (t.IsSubclassOf(typeof(TLMethod)))
            {
                ((TLMethod)obj).DeserializeResponse(reader);
                return obj;
            }
            else if (t.IsSubclassOf(typeof(TLObject)))
            {
                ((TLObject)obj).DeserializeBody(reader);
                return obj;
            }
            else throw new NotImplementedException("Weird Type : " + t.Namespace + " | " + t.Name);
        }
        public static void SerializeObject(object obj, BinaryWriter writer)
        {
            ((TLObject)obj).SerializeBody(writer);
        }
        public static TLVector<T> DeserializeVector<T>(BinaryReader reader)
        {
            if (reader.ReadInt32() != 481674261) throw new InvalidDataException("Bad Constructor");
            TLVector<T> t = new TLVector<T>();
            t.DeserializeBody(reader);
            return t;
        }
    }
}
