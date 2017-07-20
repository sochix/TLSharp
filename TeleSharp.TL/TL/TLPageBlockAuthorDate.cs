using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1162877472)]
    public class TLPageBlockAuthorDate : TLAbsPageBlock
    {
        public override int Constructor
        {
            get
            {
                return -1162877472;
            }
        }

        public TLAbsRichText author { get; set; }
        public int published_date { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            author = (TLAbsRichText)ObjectUtils.DeserializeObject(br);
            published_date = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(author, bw);
            bw.Write(published_date);

        }
    }
}
