using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-497756594)]
    public class TLRequestGetPinnedDialogs : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -497756594;
            }
        }

        public Messages.TLPeerDialogs Response { get; set; }


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
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLPeerDialogs)ObjectUtils.DeserializeObject(br);

        }
    }
}
