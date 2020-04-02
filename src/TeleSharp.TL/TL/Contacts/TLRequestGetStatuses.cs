using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Contacts
{
    [TLObject(-995929106)]
    public class TLRequestGetStatuses : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -995929106;
            }
        }

        public TLVector<TLContactStatus> Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLVector<TLContactStatus>)ObjectUtils.DeserializeVector<TLContactStatus>(br);

        }
    }
}
