using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(421243333)]
    public class TLRequestGetDialogs : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 421243333;
            }
        }

        public int flags { get; set; }
        public bool exclude_pinned { get; set; }
        public int offset_date { get; set; }
        public int offset_id { get; set; }
        public TLAbsInputPeer offset_peer { get; set; }
        public int limit { get; set; }
        public Messages.TLAbsDialogs Response { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = exclude_pinned ? (flags | 1) : (flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            exclude_pinned = (flags & 1) != 0;
            offset_date = br.ReadInt32();
            offset_id = br.ReadInt32();
            offset_peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            limit = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);

            bw.Write(offset_date);
            bw.Write(offset_id);
            ObjectUtils.SerializeObject(offset_peer, bw);
            bw.Write(limit);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLAbsDialogs)ObjectUtils.DeserializeObject(br);

        }
    }
}
