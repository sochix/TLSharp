using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(-1729618630)]
    public class TLBotInfo : TLObject
    {

		
        public override int Constructor
        {
            get
            {
                return -1729618630;
            }
        }

             public int user_id {get;set;}
     public string description {get;set;}
     public TLVector<TLBotCommand> commands {get;set;}

		public TLBotInfo (int user_id ,string description ,TLVector<TLBotCommand> commands ){
			this.user_id = user_id; 
this.description = description; 
this.commands = commands; 
	
		}
		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            user_id = br.ReadInt32();
description = StringUtil.Deserialize(br);
commands = (TLVector<TLBotCommand>)ObjectUtils.DeserializeVector<TLBotCommand>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(user_id);
StringUtil.Serialize(description,bw);
ObjectUtils.SerializeObject(commands,bw);

        }
    }
}
