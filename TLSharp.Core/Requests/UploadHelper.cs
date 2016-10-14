using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
using TeleSharp.TL.Upload;
namespace TLSharp.Core.Requests
{
    public class UploadHelper
    {
        public static async Task<TLAbsInputFile> Uploader(string name,StreamReader reader,TelegramClient client)
        {
            if (reader.BaseStream.Length < 10 * 1024 * 1024)
                return await SmallFileUpload(name, reader, client);
            else
                return await BigFileUpload(name, reader, client);
        }
        private static async Task<TLInputFile> SmallFileUpload(string name, StreamReader reader, TelegramClient client)
        {
            var file = new byte[reader.BaseStream.Length];
            reader.BaseStream.Read(file, 0, (int)reader.BaseStream.Length);
            string hash;
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                hash = Convert.ToBase64String(md5.ComputeHash(file));
            }
            reader = null;
            var stream = new MemoryStream(file);
            Queue<byte[]> parts =  new Queue<byte[]>();
            while (!(stream.Position == stream.Length))
            {
                if ((stream.Length - stream.Position) > 512 *1024)
                {
                    byte[] temp = new byte[512];
                    stream.Read(temp, 0, 512 * 1024);
                    parts.Enqueue(temp);
                }
                else
                {
                    byte[] temp = new byte[512];
                    stream.Read(temp, 0, (int)(stream.Length - stream.Position));
                    parts.Enqueue(temp);
                }
            }
            stream = null;
            GC.Collect();
            int partnumber = 0;
            long file_id = BitConverter.ToInt64(RandomByteArray(8), 0);
            while (parts.Count != 0)
            {
                var part = parts.Dequeue();
                TLRequestSaveFilePart save = new TLRequestSaveFilePart();
                save.file_id = file_id;
                save.file_part = partnumber;
                save.bytes = part;
                await client.SendRequestAsync<bool>(save);
                partnumber++;
            }
            TLInputFile returnFile = new TLInputFile();
            returnFile.id = file_id;
            returnFile.name = name;
            returnFile.parts = parts.Count;
            returnFile.md5_checksum = hash;
            return returnFile;
        }
        private static async Task<TLInputFileBig> BigFileUpload(string name, StreamReader reader, TelegramClient client)
        {
            var file = new byte[reader.BaseStream.Length];
            reader.BaseStream.Read(file, 0, (int)reader.BaseStream.Length);
            reader = null;
            var stream = new MemoryStream(file);
            Queue<byte[]> parts = new Queue<byte[]>();
            while (!(stream.Position == stream.Length))
            {
                if ((stream.Length - stream.Position) > 512 * 1024)
                {
                    byte[] temp = new byte[512];
                    stream.Read(temp, 0, 512 * 1024);
                    parts.Enqueue(temp);
                }
                else
                {
                    byte[] temp = new byte[512];
                    stream.Read(temp, 0, (int)(stream.Length - stream.Position));
                    parts.Enqueue(temp);
                }
            }
            stream = null;
            GC.Collect();
            int partnumber = 0;
            long file_id = BitConverter.ToInt64(RandomByteArray(8), 0);
            while (parts.Count != 0)
            {
                var part = parts.Dequeue();
                TLRequestSaveBigFilePart save = new TLRequestSaveBigFilePart();
                save.file_id = file_id;
                save.file_part = partnumber;
                save.bytes = part;
                save.file_total_parts = parts.Count;
                await client.SendRequestAsync<bool>(save);
                partnumber++;
            }
            TLInputFileBig returnFile = new TLInputFileBig();
            returnFile.id = file_id;
            returnFile.name = name;
            returnFile.parts = parts.Count;
            return returnFile;
        }
        private static byte[] RandomByteArray(int count)
        {
            var temp = new byte[count];
            Random random = new Random();
            random.NextBytes(temp);
            return temp;
        }
    }
}
