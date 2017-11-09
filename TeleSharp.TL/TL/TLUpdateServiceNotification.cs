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

        public int Flags { get; set; }
        public bool Popup { get; set; }
        public int? InboxDate { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public TLAbsMessageMedia Media { get; set; }
        public TLVector<TLAbsMessageEntity> Entities { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = Popup ? (Flags | 1) : (Flags & ~1);
            Flags = InboxDate != null ? (Flags | 2) : (Flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Popup = (Flags & 1) != 0;
            if ((Flags & 2) != 0)
                InboxDate = br.ReadInt32();
            else
                InboxDate = null;

            Type = StringUtil.Deserialize(br);
            Message = StringUtil.Deserialize(br);
            Media = (TLAbsMessageMedia)ObjectUtils.DeserializeObject(br);
            Entities = (TLVector<TLAbsMessageEntity>)ObjectUtils.DeserializeVector<TLAbsMessageEntity>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);

            if ((Flags & 2) != 0)
                bw.Write(InboxDate.Value);
            StringUtil.Serialize(Type, bw);
            StringUtil.Serialize(Message, bw);
            ObjectUtils.SerializeObject(Media, bw);
            ObjectUtils.SerializeObject(Entities, bw);

        }
    }
}
