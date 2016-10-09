using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
	[TLObject(-545786948)]
    public class TLRequestResetAuthorization : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -545786948;
            }
        }

                private long hash {get;set;}
        public bool Response{ get; set;}

		
		public TLRequestResetAuthorization (long hash ){
			this.hash = hash; 
	
		}

		public void ComputeFlags()
		{
			
		}

        public override void DeserializeBody(BinaryReader br)
        {
            hash = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
			bw.Write(Constructor);
            bw.Write(hash);

        }
		public override void deserializeResponse(BinaryReader br)
		{
			Response = BoolUtil.Deserialize(br);

		}
    }
}
