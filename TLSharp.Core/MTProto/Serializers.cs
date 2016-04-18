using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TLSharp.Core.MTProto
{
    public class Serializers
    {

        public static class Bytes
        {
            public static byte[] read(BinaryReader binaryReader)
            {
                byte firstByte = binaryReader.ReadByte();
                int len, padding;
                if (firstByte == 254)
                {
                    len = binaryReader.ReadByte() | (binaryReader.ReadByte() << 8) | (binaryReader.ReadByte() << 16);
                    padding = len % 4;
                }
                else {
                    len = firstByte;
                    padding = (len + 1) % 4;
                }

                byte[] data = binaryReader.ReadBytes(len);
                if (padding > 0)
                {
                    padding = 4 - padding;
                    binaryReader.ReadBytes(padding);
                }

                return data;
            }

            public static BinaryWriter write(BinaryWriter binaryWriter, byte[] data)
            {
                int padding;
                if (data.Length < 254)
                {
                    padding = (data.Length + 1) % 4;
                    if (padding != 0)
                    {
                        padding = 4 - padding;
                    }

                    binaryWriter.Write((byte)data.Length);
                    binaryWriter.Write(data);
                }
                else {
                    padding = (data.Length) % 4;
                    if (padding != 0)
                    {
                        padding = 4 - padding;
                    }

                    binaryWriter.Write((byte)254);
                    binaryWriter.Write((byte)(data.Length));
                    binaryWriter.Write((byte)(data.Length >> 8));
                    binaryWriter.Write((byte)(data.Length >> 16));
                    binaryWriter.Write(data);
                }


                for (int i = 0; i < padding; i++)
                {
                    binaryWriter.Write((byte)0);
                }

                return binaryWriter;
            }
        }

        public static class String
        {
            public static string read(BinaryReader reader)
            {
                byte[] data = Bytes.read(reader);
                return Encoding.UTF8.GetString(data, 0, data.Length);
            }

            public static BinaryWriter write(BinaryWriter writer, string str)
            {
                return Bytes.write(writer, Encoding.UTF8.GetBytes(str));
            }
        }

        public static string VectorToString<T>(List<T> list)
        {
            string[] tokens = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                tokens[i] = list[i].ToString();
            }
            return "[" + System.String.Join(", ", tokens) + "]";
        }
    }
}
