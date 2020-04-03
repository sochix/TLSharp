using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1421875280)]
    public class TLUpdateChatDefaultBannedRights : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 1421875280;
            }
        }

        public TLAbsPeer Peer { get; set; }
        public TLChatBannedRights DefaultBannedRights { get; set; }
        public int Version { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Peer = (TLAbsPeer)ObjectUtils.DeserializeObject(br);
            DefaultBannedRights = (TLChatBannedRights)ObjectUtils.DeserializeObject(br);
            Version = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Peer, bw);
            ObjectUtils.SerializeObject(DefaultBannedRights, bw);
            bw.Write(Version);

        }
    }
}
