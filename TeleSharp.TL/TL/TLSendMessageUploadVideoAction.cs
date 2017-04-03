using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
	[TLObject(-378127636)]
    public class TLSendMessageUploadVideoAction : TLAbsSendMessageAction
    {
        public override int Constructor
        {
            get
            {
                return -378127636;
            }
        }

             public int progress {get;set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            progress = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(progress);

        }
    }
}
