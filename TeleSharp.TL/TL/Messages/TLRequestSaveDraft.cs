using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-1137057461)]
    public class TLRequestSaveDraft : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1137057461;
            }
        }

        public int flags { get; set; }
        public bool no_webpage { get; set; }
        public int? reply_to_msg_id { get; set; }
        public TLAbsInputPeer peer { get; set; }
        public string message { get; set; }
        public TLVector<TLAbsMessageEntity> entities { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = no_webpage ? (flags | 2) : (flags & ~2);
            flags = reply_to_msg_id != null ? (flags | 1) : (flags & ~1);
            flags = entities != null ? (flags | 8) : (flags & ~8);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            no_webpage = (flags & 2) != 0;
            if ((flags & 1) != 0)
                reply_to_msg_id = br.ReadInt32();
            else
                reply_to_msg_id = null;

            peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            message = StringUtil.Deserialize(br);
            if ((flags & 8) != 0)
                entities = (TLVector<TLAbsMessageEntity>)ObjectUtils.DeserializeVector<TLAbsMessageEntity>(br);
            else
                entities = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);

            if ((flags & 1) != 0)
                bw.Write(reply_to_msg_id.Value);
            ObjectUtils.SerializeObject(peer, bw);
            StringUtil.Serialize(message, bw);
            if ((flags & 8) != 0)
                ObjectUtils.SerializeObject(entities, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
