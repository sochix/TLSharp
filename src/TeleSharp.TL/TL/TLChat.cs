using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1004149726)]
    public class TLChat : TLAbsChat
    {
        public override int Constructor
        {
            get
            {
                return 1004149726;
            }
        }

        public int Flags { get; set; }
        public bool Creator { get; set; }
        public bool Kicked { get; set; }
        public bool Left { get; set; }
        public bool Deactivated { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public TLAbsChatPhoto Photo { get; set; }
        public int ParticipantsCount { get; set; }
        public int Date { get; set; }
        public int Version { get; set; }
        public TLAbsInputChannel MigratedTo { get; set; }
        public TLChatAdminRights AdminRights { get; set; }
        public TLChatBannedRights DefaultBannedRights { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Creator = (Flags & 1) != 0;
            Kicked = (Flags & 2) != 0;
            Left = (Flags & 4) != 0;
            Deactivated = (Flags & 32) != 0;
            Id = br.ReadInt32();
            Title = StringUtil.Deserialize(br);
            Photo = (TLAbsChatPhoto)ObjectUtils.DeserializeObject(br);
            ParticipantsCount = br.ReadInt32();
            Date = br.ReadInt32();
            Version = br.ReadInt32();
            if ((Flags & 64) != 0)
                MigratedTo = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
            else
                MigratedTo = null;

            if ((Flags & 16384) != 0)
                AdminRights = (TLChatAdminRights)ObjectUtils.DeserializeObject(br);
            else
                AdminRights = null;

            if ((Flags & 262144) != 0)
                DefaultBannedRights = (TLChatBannedRights)ObjectUtils.DeserializeObject(br);
            else
                DefaultBannedRights = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);




            bw.Write(Id);
            StringUtil.Serialize(Title, bw);
            ObjectUtils.SerializeObject(Photo, bw);
            bw.Write(ParticipantsCount);
            bw.Write(Date);
            bw.Write(Version);
            if ((Flags & 64) != 0)
                ObjectUtils.SerializeObject(MigratedTo, bw);
            if ((Flags & 16384) != 0)
                ObjectUtils.SerializeObject(AdminRights, bw);
            if ((Flags & 262144) != 0)
                ObjectUtils.SerializeObject(DefaultBannedRights, bw);

        }
    }
}
