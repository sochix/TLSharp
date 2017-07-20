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

        public TLInputEncryptedChat peer { get; set; }
        public int max_date { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            peer = (TLInputEncryptedChat)ObjectUtils.DeserializeObject(br);
            max_date = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(peer, bw);
            bw.Write(max_date);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
