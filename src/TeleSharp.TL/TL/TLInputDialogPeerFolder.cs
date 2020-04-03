using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1684014375)]
    public class TLInputDialogPeerFolder : TLAbsInputDialogPeer
    {
        public override int Constructor
        {
            get
            {
                return 1684014375;
            }
        }

        public int FolderId { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            FolderId = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(FolderId);

        }
    }
}
