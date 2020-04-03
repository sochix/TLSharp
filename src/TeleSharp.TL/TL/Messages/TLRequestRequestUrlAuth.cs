using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-482388461)]
    public class TLRequestRequestUrlAuth : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -482388461;
            }
        }

        public TLAbsInputPeer Peer { get; set; }
        public int MsgId { get; set; }
        public int ButtonId { get; set; }
        public TLAbsUrlAuthResult Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            MsgId = br.ReadInt32();
            ButtonId = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Peer, bw);
            bw.Write(MsgId);
            bw.Write(ButtonId);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUrlAuthResult)ObjectUtils.DeserializeObject(br);

        }
    }
}
