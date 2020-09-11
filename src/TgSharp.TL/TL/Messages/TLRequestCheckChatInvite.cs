using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL.Messages
{
    [TLObject(1051570619)]
    public class TLRequestCheckChatInvite : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1051570619;
            }
        }

        public string Hash { get; set; }
        public TLAbsChatInvite Response { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Hash = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Hash, bw);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLAbsChatInvite)ObjectUtils.DeserializeObject(br);
        }
    }
}
