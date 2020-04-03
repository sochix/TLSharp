using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Phone
{
    [TLObject(1124046573)]
    public class TLRequestRequestCall : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1124046573;
            }
        }

        public int Flags { get; set; }
        public bool Video { get; set; }
        public TLAbsInputUser UserId { get; set; }
        public int RandomId { get; set; }
        public byte[] GAHash { get; set; }
        public TLPhoneCallProtocol Protocol { get; set; }
        public Phone.TLPhoneCall Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Video = (Flags & 1) != 0;
            UserId = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
            RandomId = br.ReadInt32();
            GAHash = BytesUtil.Deserialize(br);
            Protocol = (TLPhoneCallProtocol)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);

            ObjectUtils.SerializeObject(UserId, bw);
            bw.Write(RandomId);
            BytesUtil.Serialize(GAHash, bw);
            ObjectUtils.SerializeObject(Protocol, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Phone.TLPhoneCall)ObjectUtils.DeserializeObject(br);

        }
    }
}
