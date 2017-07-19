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

        public TLAbsInputUser bot { get; set; }
        public TLAbsInputPeer peer { get; set; }
        public long random_id { get; set; }
        public string start_param { get; set; }
        public TLAbsUpdates Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            bot = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
            peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            random_id = br.ReadInt64();
            start_param = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(bot, bw);
            ObjectUtils.SerializeObject(peer, bw);
            bw.Write(random_id);
            StringUtil.Serialize(start_param, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

        }
    }
}
