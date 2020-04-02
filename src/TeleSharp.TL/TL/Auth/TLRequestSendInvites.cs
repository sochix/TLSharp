using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Auth
{
    [TLObject(1998331287)]
    public class TLRequestSendInvites : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1998331287;
            }
        }

        public TLVector<string> PhoneNumbers { get; set; }
        public string Message { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            PhoneNumbers = (TLVector<string>)ObjectUtils.DeserializeVector<string>(br);
            Message = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(PhoneNumbers, bw);
            StringUtil.Serialize(Message, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
