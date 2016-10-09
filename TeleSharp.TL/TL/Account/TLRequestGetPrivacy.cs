using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
	[TLObject(-623130288)]
    public class TLRequestGetPrivacy : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -623130288;
            }
        }

                private TLAbsInputPrivacyKey key {get;set;}
        public Account.TLPrivacyRules Response{ get; set;}

		
		public TLRequestGetPrivacy (TLAbsInputPrivacyKey key ){
			this.key = key; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            key = (TLAbsInputPrivacyKey)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(key,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (Account.TLPrivacyRules)ObjectUtils.DeserializeObject(br);

		}
    }
}
