using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(847887978)]
    public class TLRequestToggleDialogPin : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 847887978;
            }
        }

        public int flags { get; set; }
        public bool pinned { get; set; }
        public TLAbsInputPeer peer { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = pinned ? (flags | 1) : (flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            pinned = (flags & 1) != 0;
            peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);

            ObjectUtils.SerializeObject(peer, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
