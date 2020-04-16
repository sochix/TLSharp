using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL.Contacts
{
    [TLObject(-113456221)]
    public class TLRequestResolveUsername : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -113456221;
            }
        }

        public string Username { get; set; }
        public Contacts.TLResolvedPeer Response { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Username = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Username, bw);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Contacts.TLResolvedPeer)ObjectUtils.DeserializeObject(br);
        }
    }
}
