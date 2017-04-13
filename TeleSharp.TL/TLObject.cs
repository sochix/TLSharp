using System;
using System.IO;

namespace TeleSharp.TL
{
    public class TLObjectAttribute : Attribute
    {
        public TLObjectAttribute(int Constructor)
        {
            this.Constructor = Constructor;
        }

        public int Constructor { get; }
    }

    public abstract class TLObject
    {
        public abstract int Constructor { get; }
        public abstract void SerializeBody(BinaryWriter bw);
        public abstract void DeserializeBody(BinaryReader br);

        public byte[] Serialize()
        {
            using (var m = new MemoryStream())
            using (var bw = new BinaryWriter(m))
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

        public void Deserialize(BinaryReader reader)
        {
            var constructorId = reader.ReadInt32();
            if (constructorId != Constructor)
                throw new InvalidDataException("Constructor Invalid");
            DeserializeBody(reader);
        }
    }
}