using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Contacts
{
    [TLObject(301470424)]
    public class TLRequestSearch : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 301470424;
            }
        }

        public string q { get; set; }
        public int limit { get; set; }
        public Contacts.TLFound Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            q = StringUtil.Deserialize(br);
            limit = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(q, bw);
            bw.Write(limit);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (Contacts.TLFound)ObjectUtils.DeserializeObject(br);

        }
    }
}
