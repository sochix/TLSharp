using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Contacts
{
    [TLObject(-176409329)]
    public class TLRequestGetBlocked : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -176409329;
            }
        }

        public int offset { get; set; }
        public int limit { get; set; }
        public Contacts.TLAbsBlocked Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            offset = br.ReadInt32();
            limit = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(offset);
            bw.Write(limit);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (Contacts.TLAbsBlocked)ObjectUtils.DeserializeObject(br);

        }
    }
}
