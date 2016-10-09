using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
	[TLObject(-906486552)]
    public class TLRequestSetPrivacy : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -906486552;
            }
        }

                private TLAbsInputPrivacyKey key {get;set;}
        private TLVector<TLAbsInputPrivacyRule> rules {get;set;}
        public Account.TLPrivacyRules Response{ get; set;}

		
		public TLRequestSetPrivacy (TLAbsInputPrivacyKey key ,TLVector<TLAbsInputPrivacyRule> rules ){
			this.key = key; 
this.rules = rules; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            key = (TLAbsInputPrivacyKey)ObjectUtils.DeserializeObject(br);
rules = (TLVector<TLAbsInputPrivacyRule>)ObjectUtils.DeserializeVector<TLAbsInputPrivacyRule>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            ObjectUtils.SerializeObject(key,bw);
ObjectUtils.SerializeObject(rules,bw);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = (Account.TLPrivacyRules)ObjectUtils.DeserializeObject(br);

		}
    }
}
