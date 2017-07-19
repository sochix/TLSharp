using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-75283823)]
    public class TLTopPeerCategoryPeers : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -75283823;
            }
        }

        public TLAbsTopPeerCategory category { get; set; }
        public int count { get; set; }
        public TLVector<TLTopPeer> peers { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            category = (TLAbsTopPeerCategory)ObjectUtils.DeserializeObject(br);
            count = br.ReadInt32();
            peers = (TLVector<TLTopPeer>)ObjectUtils.DeserializeVector<TLTopPeer>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(category, bw);
            bw.Write(count);
            ObjectUtils.SerializeObject(peers, bw);

        }
    }
}
