using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Phone
{
    [TLObject(1536537556)]
    public class TLRequestRequestCall : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1536537556;
            }
        }

        public TLAbsInputUser user_id { get; set; }
        public int random_id { get; set; }
        public byte[] g_a_hash { get; set; }
        public TLPhoneCallProtocol protocol { get; set; }
        public Phone.TLPhoneCall Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            user_id = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
            random_id = br.ReadInt32();
            g_a_hash = BytesUtil.Deserialize(br);
            protocol = (TLPhoneCallProtocol)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(user_id, bw);
            bw.Write(random_id);
            BytesUtil.Serialize(g_a_hash, bw);
            ObjectUtils.SerializeObject(protocol, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (Phone.TLPhoneCall)ObjectUtils.DeserializeObject(br);

        }
    }
}
