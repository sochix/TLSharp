using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(852769188)]
    public class TLRequestSendEncryptedService : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 852769188;
            }
        }

        public TLInputEncryptedChat Peer { get; set; }
        public long RandomId { get; set; }
        public byte[] Data { get; set; }
        public Messages.TLAbsSentEncryptedMessage Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Peer = (TLInputEncryptedChat)ObjectUtils.DeserializeObject(br);
            RandomId = br.ReadInt64();
            Data = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Peer, bw);
            bw.Write(RandomId);
            BytesUtil.Serialize(Data, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLAbsSentEncryptedMessage)ObjectUtils.DeserializeObject(br);

        }
    }
}
