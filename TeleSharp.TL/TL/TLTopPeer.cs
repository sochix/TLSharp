using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-305282981)]
    public class TLTopPeer : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -305282981;
            }
        }

        public TLAbsPeer peer { get; set; }
        public double rating { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            peer = (TLAbsPeer)ObjectUtils.DeserializeObject(br);
            rating = br.ReadDouble();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(peer, bw);
            bw.Write(rating);

        }
    }
}
