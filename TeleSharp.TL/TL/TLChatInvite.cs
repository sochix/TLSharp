using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-613092008)]
    public class TLChatInvite : TLAbsChatInvite
    {
        public override int Constructor
        {
            get
            {
                return -613092008;
            }
        }

        public int flags { get; set; }
        public bool channel { get; set; }
        public bool broadcast { get; set; }
        public bool @public { get; set; }
        public bool megagroup { get; set; }
        public string title { get; set; }
        public TLAbsChatPhoto photo { get; set; }
        public int participants_count { get; set; }
        public TLVector<TLAbsUser> participants { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = channel ? (flags | 1) : (flags & ~1);
            flags = broadcast ? (flags | 2) : (flags & ~2);
            flags = @public ? (flags | 4) : (flags & ~4);
            flags = megagroup ? (flags | 8) : (flags & ~8);
            flags = participants != null ? (flags | 16) : (flags & ~16);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            channel = (flags & 1) != 0;
            broadcast = (flags & 2) != 0;
            @public = (flags & 4) != 0;
            megagroup = (flags & 8) != 0;
            title = StringUtil.Deserialize(br);
            photo = (TLAbsChatPhoto)ObjectUtils.DeserializeObject(br);
            participants_count = br.ReadInt32();
            if ((flags & 16) != 0)
                participants = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);
            else
                participants = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);




            StringUtil.Serialize(title, bw);
            ObjectUtils.SerializeObject(photo, bw);
            bw.Write(participants_count);
            if ((flags & 16) != 0)
                ObjectUtils.SerializeObject(participants, bw);

        }
    }
}
