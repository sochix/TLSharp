using System;
using System.IO;

namespace TeleSharp.TL
{
    public abstract class TLObject
    {
        public abstract int Constructor { get; }

        public void Deserialize(BinaryReader reader)
        {
            int constructorId = reader.ReadInt32();
            if (constructorId != Constructor)
                throw new InvalidDataException("Constructor Invalid");
            DeserializeBody(reader);
        }

        public abstract void DeserializeBody(BinaryReader br);

        public byte[] Serialize()
        {
            using (MemoryStream m = new MemoryStream())
            using (BinaryWriter bw = new BinaryWriter(m))
            {
                Serialize(bw);
                bw.Close();
                m.Close();
                return m.GetBuffer();
            }
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Constructor);
            SerializeBody(writer);
        }

        public abstract void SerializeBody(BinaryWriter bw);
    }

    public class TLObjectAttribute : Attribute
    {
        public TLObjectAttribute(int Constructor)
        {
            this.Constructor = Constructor;
        }

        public int Constructor { get; private set; }
    }
}
