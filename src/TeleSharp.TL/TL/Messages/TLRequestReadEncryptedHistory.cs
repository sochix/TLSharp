using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(2135648522)]
    public class TLRequestReadEncryptedHistory : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 2135648522;
            }
        }

        public TLInputEncryptedChat Peer { get; set; }
        public int MaxDate { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Peer = (TLInputEncryptedChat)ObjectUtils.DeserializeObject(br);
            MaxDate = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Peer, bw);
            bw.Write(MaxDate);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
