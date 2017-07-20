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

        public int id { get; set; }
        public TLAbsChatParticipants participants { get; set; }
        public TLAbsPhoto chat_photo { get; set; }
        public TLAbsPeerNotifySettings notify_settings { get; set; }
        public TLAbsExportedChatInvite exported_invite { get; set; }
        public TLVector<TLBotInfo> bot_info { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = br.ReadInt32();
            participants = (TLAbsChatParticipants)ObjectUtils.DeserializeObject(br);
            chat_photo = (TLAbsPhoto)ObjectUtils.DeserializeObject(br);
            notify_settings = (TLAbsPeerNotifySettings)ObjectUtils.DeserializeObject(br);
            exported_invite = (TLAbsExportedChatInvite)ObjectUtils.DeserializeObject(br);
            bot_info = (TLVector<TLBotInfo>)ObjectUtils.DeserializeVector<TLBotInfo>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(id);
            ObjectUtils.SerializeObject(participants, bw);
            ObjectUtils.SerializeObject(chat_photo, bw);
            ObjectUtils.SerializeObject(notify_settings, bw);
            ObjectUtils.SerializeObject(exported_invite, bw);
            ObjectUtils.SerializeObject(bot_info, bw);

        }
    }
}
