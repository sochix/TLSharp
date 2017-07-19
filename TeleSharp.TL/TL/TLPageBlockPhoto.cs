using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-372860542)]
    public class TLPageBlockPhoto : TLAbsPageBlock
    {
        public override int Constructor
        {
            get
            {
                return -372860542;
            }
        }

        public long photo_id { get; set; }
        public TLAbsRichText caption { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            photo_id = br.ReadInt64();
            caption = (TLAbsRichText)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(photo_id);
            ObjectUtils.SerializeObject(caption, bw);

        }
    }
}
