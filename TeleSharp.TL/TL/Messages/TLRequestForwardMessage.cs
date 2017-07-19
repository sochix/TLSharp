using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(865483769)]
    public class TLRequestForwardMessage : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 865483769;
            }
        }

        public TLAbsInputPeer peer { get; set; }
        public int id { get; set; }
        public long random_id { get; set; }
        public TLAbsUpdates Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            id = br.ReadInt32();
            random_id = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(peer, bw);
            bw.Write(id);
            bw.Write(random_id);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

        }
    }
}
