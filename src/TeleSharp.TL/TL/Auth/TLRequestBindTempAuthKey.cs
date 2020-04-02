using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Auth
{
    [TLObject(-841733627)]
    public class TLRequestBindTempAuthKey : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -841733627;
            }
        }

        public long PermAuthKeyId { get; set; }
        public long Nonce { get; set; }
        public int ExpiresAt { get; set; }
        public byte[] EncryptedMessage { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            PermAuthKeyId = br.ReadInt64();
            Nonce = br.ReadInt64();
            ExpiresAt = br.ReadInt32();
            EncryptedMessage = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(PermAuthKeyId);
            bw.Write(Nonce);
            bw.Write(ExpiresAt);
            BytesUtil.Serialize(EncryptedMessage, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
