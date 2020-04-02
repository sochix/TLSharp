using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Auth
{
    [TLObject(-470837741)]
    public class TLRequestImportAuthorization : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -470837741;
            }
        }

        public int Id { get; set; }
        public byte[] Bytes { get; set; }
        public Auth.TLAuthorization Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Id = br.ReadInt32();
            Bytes = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Id);
            BytesUtil.Serialize(Bytes, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Auth.TLAuthorization)ObjectUtils.DeserializeObject(br);

        }
    }
}
