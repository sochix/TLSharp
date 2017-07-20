using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Channels
{
    [TLObject(-950663035)]
    public class TLRequestExportInvite : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -950663035;
            }
        }

        public TLAbsInputChannel channel { get; set; }
        public TLAbsExportedChatInvite Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(channel, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsExportedChatInvite)ObjectUtils.DeserializeObject(br);

        }
    }
}
