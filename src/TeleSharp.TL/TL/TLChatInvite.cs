using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-540871282)]
    public class TLChatInvite : TLAbsChatInvite
    {
        public override int Constructor
        {
            get
            {
                return -540871282;
            }
        }

        public int Flags { get; set; }
        public bool Channel { get; set; }
        public bool Broadcast { get; set; }
        public bool Public { get; set; }
        public bool Megagroup { get; set; }
        public string Title { get; set; }
        public TLAbsPhoto Photo { get; set; }
        public int ParticipantsCount { get; set; }
        public TLVector<TLAbsUser> Participants { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Channel = (Flags & 1) != 0;
            Broadcast = (Flags & 2) != 0;
            Public = (Flags & 4) != 0;
            Megagroup = (Flags & 8) != 0;
            Title = StringUtil.Deserialize(br);
            Photo = (TLAbsPhoto)ObjectUtils.DeserializeObject(br);
            ParticipantsCount = br.ReadInt32();
            if ((Flags & 16) != 0)
                Participants = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);
            else
                Participants = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);




            StringUtil.Serialize(Title, bw);
            ObjectUtils.SerializeObject(Photo, bw);
            bw.Write(ParticipantsCount);
            if ((Flags & 16) != 0)
                ObjectUtils.SerializeObject(Participants, bw);

        }
    }
}
