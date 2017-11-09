using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(1158290442)]
    public class TLFoundGifs : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 1158290442;
            }
        }

        public int NextOffset { get; set; }
        public TLVector<TLAbsFoundGif> Results { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            NextOffset = br.ReadInt32();
            Results = (TLVector<TLAbsFoundGif>)ObjectUtils.DeserializeVector<TLAbsFoundGif>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(NextOffset);
            ObjectUtils.SerializeObject(Results, bw);

        }
    }
}
