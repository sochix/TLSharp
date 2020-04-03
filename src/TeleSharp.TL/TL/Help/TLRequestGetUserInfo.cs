using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Help
{
    [TLObject(59377875)]
    public class TLRequestGetUserInfo : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 59377875;
            }
        }

        public TLAbsInputUser UserId { get; set; }
        public Help.TLAbsUserInfo Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            UserId = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(UserId, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Help.TLAbsUserInfo)ObjectUtils.DeserializeObject(br);

        }
    }
}
