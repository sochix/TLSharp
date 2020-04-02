using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-993483427)]
    public class TLRequestGetMessagesViews : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -993483427;
            }
        }

        public TLAbsInputPeer Peer { get; set; }
        public TLVector<int> Id { get; set; }
        public bool Increment { get; set; }
        public TLVector<int> Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            Id = (TLVector<int>)ObjectUtils.DeserializeVector<int>(br);
            Increment = BoolUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Peer, bw);
            ObjectUtils.SerializeObject(Id, bw);
            BoolUtil.Serialize(Increment, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLVector<int>)ObjectUtils.DeserializeVector<int>(br);

        }
    }
}
