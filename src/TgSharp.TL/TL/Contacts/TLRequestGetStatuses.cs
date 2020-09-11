using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL.Contacts
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
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            // do nothing
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            // do nothing else
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLVector<TLContactStatus>)ObjectUtils.DeserializeVector<TLContactStatus>(br);
        }
    }
}
