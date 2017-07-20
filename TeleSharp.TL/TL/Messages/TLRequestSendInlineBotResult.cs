using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-1318189314)]
    public class TLRequestSendInlineBotResult : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1318189314;
            }
        }

        public int flags { get; set; }
        public bool silent { get; set; }
        public bool background { get; set; }
        public bool clear_draft { get; set; }
        public TLAbsInputPeer peer { get; set; }
        public int? reply_to_msg_id { get; set; }
        public long random_id { get; set; }
        public long query_id { get; set; }
        public string id { get; set; }
        public TLAbsUpdates Response { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = silent ? (flags | 32) : (flags & ~32);
            flags = background ? (flags | 64) : (flags & ~64);
            flags = clear_draft ? (flags | 128) : (flags & ~128);
            flags = reply_to_msg_id != null ? (flags | 1) : (flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            silent = (flags & 32) != 0;
            background = (flags & 64) != 0;
            clear_draft = (flags & 128) != 0;
            peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            if ((flags & 1) != 0)
                reply_to_msg_id = br.ReadInt32();
            else
                reply_to_msg_id = null;

            random_id = br.ReadInt64();
            query_id = br.ReadInt64();
            id = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);



            ObjectUtils.SerializeObject(peer, bw);
            if ((flags & 1) != 0)
                bw.Write(reply_to_msg_id.Value);
            bw.Write(random_id);
            bw.Write(query_id);
            StringUtil.Serialize(id, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

        }
    }
}
