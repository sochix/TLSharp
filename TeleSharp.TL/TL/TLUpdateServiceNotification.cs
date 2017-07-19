using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-337352679)]
    public class TLUpdateServiceNotification : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -337352679;
            }
        }

        public int flags { get; set; }
        public bool popup { get; set; }
        public int? inbox_date { get; set; }
        public string type { get; set; }
        public string message { get; set; }
        public TLAbsMessageMedia media { get; set; }
        public TLVector<TLAbsMessageEntity> entities { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = popup ? (flags | 1) : (flags & ~1);
            flags = inbox_date != null ? (flags | 2) : (flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            popup = (flags & 1) != 0;
            if ((flags & 2) != 0)
                inbox_date = br.ReadInt32();
            else
                inbox_date = null;

            type = StringUtil.Deserialize(br);
            message = StringUtil.Deserialize(br);
            media = (TLAbsMessageMedia)ObjectUtils.DeserializeObject(br);
            entities = (TLVector<TLAbsMessageEntity>)ObjectUtils.DeserializeVector<TLAbsMessageEntity>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);

            if ((flags & 2) != 0)
                bw.Write(inbox_date.Value);
            StringUtil.Serialize(type, bw);
            StringUtil.Serialize(message, bw);
            ObjectUtils.SerializeObject(media, bw);
            ObjectUtils.SerializeObject(entities, bw);

        }
    }
}
