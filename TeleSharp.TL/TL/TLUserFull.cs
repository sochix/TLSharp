using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(1496513539)]
    public class TLUserFull : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 1496513539;
            }
        }

             public int flags {get;set;}
     public bool blocked {get;set;}
     public TLAbsUser user {get;set;}
     public string about {get;set;}
     public Contacts.TLLink link {get;set;}
     public TLAbsPhoto profile_photo {get;set;}
     public TLAbsPeerNotifySettings notify_settings {get;set;}
     public TLBotInfo bot_info {get;set;}


		public void ComputeFlags()
		{
			flags = 0;
flags = blocked ? (flags | 1) : (flags & ~1);
flags = about != null ? (flags | 2) : (flags & ~2);
flags = profile_photo != null ? (flags | 4) : (flags & ~4);
flags = bot_info != null ? (flags | 8) : (flags & ~8);

		}

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
blocked = (flags & 1) != 0;
user = (TLAbsUser)ObjectUtils.DeserializeObject(br);
if ((flags & 2) != 0)
about = StringUtil.Deserialize(br);
else
about = null;

link = (Contacts.TLLink)ObjectUtils.DeserializeObject(br);
if ((flags & 4) != 0)
profile_photo = (TLAbsPhoto)ObjectUtils.DeserializeObject(br);
else
profile_photo = null;

notify_settings = (TLAbsPeerNotifySettings)ObjectUtils.DeserializeObject(br);
if ((flags & 8) != 0)
bot_info = (TLBotInfo)ObjectUtils.DeserializeObject(br);
else
bot_info = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ComputeFlags();
bw.Write(flags);

ObjectUtils.SerializeObject(user,bw);
if ((flags & 2) != 0)
StringUtil.Serialize(about,bw);
ObjectUtils.SerializeObject(link,bw);
if ((flags & 4) != 0)
ObjectUtils.SerializeObject(profile_photo,bw);
ObjectUtils.SerializeObject(notify_settings,bw);
if ((flags & 8) != 0)
ObjectUtils.SerializeObject(bot_info,bw);

        }
    }
}
