using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-421563528)]
    public class TLRequestStartBot : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -421563528;
            }
        }

        public TLAbsInputUser Bot { get; set; }
        public TLAbsInputPeer Peer { get; set; }
        public long RandomId { get; set; }
        public string StartParam { get; set; }
        public TLAbsUpdates Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Bot = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
            Peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            RandomId = br.ReadInt64();
            StartParam = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Bot, bw);
            ObjectUtils.SerializeObject(Peer, bw);
            bw.Write(RandomId);
            StringUtil.Serialize(StartParam, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

        }
    }
}
