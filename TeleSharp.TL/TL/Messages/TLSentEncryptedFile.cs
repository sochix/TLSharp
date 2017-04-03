using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
	[TLObject(-1802240206)]
    public class TLSentEncryptedFile : TLAbsSentEncryptedMessage
    {
        public override int Constructor
        {
            get
            {
                return -1802240206;
            }
        }

             public int date {get;set;}
     public TLAbsEncryptedFile file {get;set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            date = br.ReadInt32();
file = (TLAbsEncryptedFile)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(date);
ObjectUtils.SerializeObject(file,bw);

        }
    }
}
