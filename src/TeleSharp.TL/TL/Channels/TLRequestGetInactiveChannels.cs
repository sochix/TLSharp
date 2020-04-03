using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Channels
{
    [TLObject(300429806)]
    public class TLRequestGetInactiveChannels : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 300429806;
            }
        }

        public Messages.TLInactiveChats Response { get; set; }


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
            Response = (Messages.TLInactiveChats)ObjectUtils.DeserializeObject(br);

        }
    }
}
