using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(771925524)]
    public class TLChatFull : TLAbsChatFull
    {
        public override int Constructor
        {
            get
            {
                return 771925524;
            }
        }

        public int Id { get; set; }
        public TLAbsChatParticipants Participants { get; set; }
        public TLAbsPhoto ChatPhoto { get; set; }
        public TLAbsPeerNotifySettings NotifySettings { get; set; }
        public TLAbsExportedChatInvite ExportedInvite { get; set; }
        public TLVector<TLBotInfo> BotInfo { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Id = br.ReadInt32();
            Participants = (TLAbsChatParticipants)ObjectUtils.DeserializeObject(br);
            ChatPhoto = (TLAbsPhoto)ObjectUtils.DeserializeObject(br);
            NotifySettings = (TLAbsPeerNotifySettings)ObjectUtils.DeserializeObject(br);
            ExportedInvite = (TLAbsExportedChatInvite)ObjectUtils.DeserializeObject(br);
            BotInfo = (TLVector<TLBotInfo>)ObjectUtils.DeserializeVector<TLBotInfo>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Id);
            ObjectUtils.SerializeObject(Participants, bw);
            ObjectUtils.SerializeObject(ChatPhoto, bw);
            ObjectUtils.SerializeObject(NotifySettings, bw);
            ObjectUtils.SerializeObject(ExportedInvite, bw);
            ObjectUtils.SerializeObject(BotInfo, bw);

        }
    }
}
