using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(422972864)]
    public class TLUpdateFolderPeers : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 422972864;
            }
        }

        public TLVector<TLFolderPeer> FolderPeers { get; set; }
        public int Pts { get; set; }
        public int PtsCount { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            FolderPeers = (TLVector<TLFolderPeer>)ObjectUtils.DeserializeVector<TLFolderPeer>(br);
            Pts = br.ReadInt32();
            PtsCount = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(FolderPeers, bw);
            bw.Write(Pts);
            bw.Write(PtsCount);

        }
    }
}
