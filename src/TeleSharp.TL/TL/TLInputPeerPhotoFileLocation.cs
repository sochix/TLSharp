using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(668375447)]
    public class TLInputPeerPhotoFileLocation : TLAbsInputFileLocation
    {
        public override int Constructor
        {
            get
            {
                return 668375447;
            }
        }

        public int Flags { get; set; }
        public bool Big { get; set; }
        public TLAbsInputPeer Peer { get; set; }
        public long VolumeId { get; set; }
        public int LocalId { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Big = (Flags & 1) != 0;
            Peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            VolumeId = br.ReadInt64();
            LocalId = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);

            ObjectUtils.SerializeObject(Peer, bw);
            bw.Write(VolumeId);
            bw.Write(LocalId);

        }
    }
}
