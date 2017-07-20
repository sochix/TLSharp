using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Contacts
{
    [TLObject(1340184318)]
    public class TLRequestImportCard : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1340184318;
            }
        }

        public TLVector<int> export_card { get; set; }
        public TLAbsUser Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            export_card = (TLVector<int>)ObjectUtils.DeserializeVector<int>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(export_card, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUser)ObjectUtils.DeserializeObject(br);

        }
    }
}
