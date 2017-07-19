using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Contacts
{
    [TLObject(583445000)]
    public class TLRequestGetContacts : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 583445000;
            }
        }

        public string hash { get; set; }
        public Contacts.TLAbsContacts Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            hash = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(hash, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (Contacts.TLAbsContacts)ObjectUtils.DeserializeObject(br);

        }
    }
}
