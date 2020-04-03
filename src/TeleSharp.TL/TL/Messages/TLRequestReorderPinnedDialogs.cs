using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(991616823)]
    public class TLRequestReorderPinnedDialogs : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 991616823;
            }
        }

        public int Flags { get; set; }
        public bool Force { get; set; }
        public int FolderId { get; set; }
        public TLVector<TLAbsInputDialogPeer> Order { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Force = (Flags & 1) != 0;
            FolderId = br.ReadInt32();
            Order = (TLVector<TLAbsInputDialogPeer>)ObjectUtils.DeserializeVector<TLAbsInputDialogPeer>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);

            bw.Write(FolderId);
            ObjectUtils.SerializeObject(Order, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
