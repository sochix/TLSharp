using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-829299510)]
    public class TLRequestEditMessage : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -829299510;
            }
        }

        public int flags { get; set; }
        public bool no_webpage { get; set; }
        public TLAbsInputPeer peer { get; set; }
        public int id { get; set; }
        public string message { get; set; }
        public TLAbsReplyMarkup reply_markup { get; set; }
        public TLVector<TLAbsMessageEntity> entities { get; set; }
        public TLAbsUpdates Response { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = no_webpage ? (flags | 2) : (flags & ~2);
            flags = message != null ? (flags | 2048) : (flags & ~2048);
            flags = reply_markup != null ? (flags | 4) : (flags & ~4);
            flags = entities != null ? (flags | 8) : (flags & ~8);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            no_webpage = (flags & 2) != 0;
            peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            id = br.ReadInt32();
            if ((flags & 2048) != 0)
                message = StringUtil.Deserialize(br);
            else
                message = null;

            if ((flags & 4) != 0)
                reply_markup = (TLAbsReplyMarkup)ObjectUtils.DeserializeObject(br);
            else
                reply_markup = null;

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

            ObjectUtils.SerializeObject(peer, bw);
            bw.Write(id);
            if ((flags & 2048) != 0)
                StringUtil.Serialize(message, bw);
            if ((flags & 4) != 0)
                ObjectUtils.SerializeObject(reply_markup, bw);
            if ((flags & 8) != 0)
                ObjectUtils.SerializeObject(entities, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

        }
    }
}
