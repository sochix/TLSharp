
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TeleSharp.TL;
namespace TeleSharp.TL
{
    public class IntegerUtil
    {
        public static int Deserialize(BinaryReader reader)
        {
            return reader.ReadInt32();
        }

        public static void Serialize(int src, BinaryWriter writer)
        {
            writer.Write(src);
        }
    }
    public class BytesUtil
    {
        private static byte[] read(BinaryReader binaryReader)
        {
            byte firstByte = binaryReader.ReadByte();
            int len, padding;
            if (firstByte == 254)
            {
                len = binaryReader.ReadByte() | (binaryReader.ReadByte() << 8) | (binaryReader.ReadByte() << 16);
                padding = len % 4;
            }
            else
            {
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

        private static BinaryWriter write(BinaryWriter binaryWriter, byte[] data)
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
            else
            {
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
        public static byte[] Deserialize(BinaryReader reader)
        {
            return read(reader);
        }

        public static void Serialize(byte[] src, BinaryWriter writer)
        {
            write(writer, src);
        }
    }
    public class StringUtil
    {
        public static string Deserialize(BinaryReader reader)
        {
            byte[] data = BytesUtil.Deserialize(reader);
            return Encoding.UTF8.GetString(data, 0, data.Length);
        }
        public static void Serialize(string src, BinaryWriter writer)
        {
            BytesUtil.Serialize(Encoding.UTF8.GetBytes(src), writer);
        }
    }
    public class BoolUtil
    {
        public static bool Deserialize(BinaryReader reader)
        {
            var FalseCNumber = -1132882121;
            var TrueCNumber = -1720552011;
            var readed = reader.ReadInt32();
            if (readed == FalseCNumber) return false;
            else if (readed == TrueCNumber) return true;
            else throw new InvalidDataException(String.Format("Invalid Boolean Data : {0}", readed.ToString()));
        }
        public static void Serialize(bool src, BinaryWriter writer)
        {
            var FalseCNumber = -1132882121;
            var TrueCNumber = -1720552011;
            writer.Write(src ? TrueCNumber : FalseCNumber);
        }
    }
    public class UIntUtil
    {
        public static uint Deserialize(BinaryReader reader)
        {
            return reader.ReadUInt32();
        }
        public static void Serialize(uint src, BinaryWriter writer)
        {
            writer.Write(src);
        }
    }
    public class DoubleUtil
    {
        public static double Deserialize(BinaryReader reader)
        {
            return reader.ReadDouble();
        }
        public static void Serialize(double src, BinaryWriter writer)
        {
            writer.Write(src);
        }
    }
    public class LongUtil
    {
        public static long Deserialize(BinaryReader reader)
        {
            return reader.ReadInt64();
        }
        public static void Serialize(long src, BinaryWriter writer)
        {
            writer.Write(src);
        }
    }
}
