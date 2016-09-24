using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Help
{
	[TLObject(-1189013126)]
    public class TLRequestGetAppChangelog : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1189013126;
            }
        }

                public Help.TLAbsAppChangelog Response{ get; set;}


		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            
        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            
        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (Help.TLAbsAppChangelog)ObjectUtils.DeserializeObject(br);

		}
    }
}
