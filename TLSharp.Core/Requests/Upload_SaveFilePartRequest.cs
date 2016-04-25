using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLSharp.Core.MTProto;

namespace TLSharp.Core.Requests
{
    //upload.saveFilePart#b304a621 file_id:long file_part:int bytes:bytes = Bool;
    public class Upload_SaveFilePartRequest : MTProtoRequest
    {
        long file_id;
        int file_part;
        byte[] bytes;

        public bool Done { get; set; }

        public Upload_SaveFilePartRequest(long file_id, int file_part, byte[] bytes)
        {
            this.file_id = file_id;
            this.file_part = file_part;
            this.bytes = bytes;
        }

        public override void OnResponse(BinaryReader reader)
        {
            var code = reader.ReadUInt32();

            if (code != 0xbc799737 && code != 0x997275b5)
                throw new InvalidOperationException($"Expected Tl Bool type");

            Done = code == 0x997275b5 ? true : false;
        }

        public override void OnSend(BinaryWriter writer)
        {
            writer.Write(0xb304a621);
            writer.Write(file_id);
            writer.Write(file_part);
            Serializers.Bytes.write(writer, bytes);
        }

        public override void OnException(Exception exception)
        {
            throw new NotImplementedException();
        }

        public override bool Confirmed => true;
        public override bool Responded { get; }
    }
}
