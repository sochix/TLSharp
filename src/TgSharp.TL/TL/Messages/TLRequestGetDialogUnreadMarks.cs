using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL.Messages
{
    [TLObject(585256482)]
    public class TLRequestGetDialogUnreadMarks : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 585256482;
            }
        }

        public TLVector<TLAbsDialogPeer> Response { get; set; }

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
            Response = (TLVector<TLAbsDialogPeer>)ObjectUtils.DeserializeVector<TLAbsDialogPeer>(br);
        }
    }
}
