using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(1035731989)]
    public class TLRequestAcceptEncryption : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1035731989;
            }
        }

        public TLInputEncryptedChat peer { get; set; }
        public byte[] g_b { get; set; }
        public long key_fingerprint { get; set; }
        public TLAbsEncryptedChat Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            peer = (TLInputEncryptedChat)ObjectUtils.DeserializeObject(br);
            g_b = BytesUtil.Deserialize(br);
            key_fingerprint = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(peer, bw);
            BytesUtil.Serialize(g_b, bw);
            bw.Write(key_fingerprint);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsEncryptedChat)ObjectUtils.DeserializeObject(br);

        }
    }
}
