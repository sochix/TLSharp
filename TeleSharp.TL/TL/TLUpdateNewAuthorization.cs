using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(-1895411046)]
    public class TLUpdateNewAuthorization : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -1895411046;
            }
        }

             public long auth_key_id {get;set;}
     public int date {get;set;}
     public string device {get;set;}
     public string location {get;set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            auth_key_id = br.ReadInt64();
date = br.ReadInt32();
device = StringUtil.Deserialize(br);
location = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(auth_key_id);
bw.Write(date);
StringUtil.Serialize(device,bw);
StringUtil.Serialize(location,bw);

        }
    }
}
