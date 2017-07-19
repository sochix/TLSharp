using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Users
{
    [TLObject(-902781519)]
    public class TLRequestGetFullUser : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -902781519;
            }
        }

        public TLAbsInputUser id { get; set; }
        public TLUserFull Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(id, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLUserFull)ObjectUtils.DeserializeObject(br);

        }
    }
}
