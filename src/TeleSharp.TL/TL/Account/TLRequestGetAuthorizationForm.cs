using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(-1200903967)]
    public class TLRequestGetAuthorizationForm : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1200903967;
            }
        }

        public int BotId { get; set; }
        public string Scope { get; set; }
        public string PublicKey { get; set; }
        public Account.TLAuthorizationForm Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            BotId = br.ReadInt32();
            Scope = StringUtil.Deserialize(br);
            PublicKey = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(BotId);
            StringUtil.Serialize(Scope, bw);
            StringUtil.Serialize(PublicKey, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Account.TLAuthorizationForm)ObjectUtils.DeserializeObject(br);

        }
    }
}
