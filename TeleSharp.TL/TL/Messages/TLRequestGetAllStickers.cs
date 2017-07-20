using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(479598769)]
    public class TLRequestGetAllStickers : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 479598769;
            }
        }

        public int hash { get; set; }
        public Messages.TLAbsAllStickers Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            hash = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(hash);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLAbsAllStickers)ObjectUtils.DeserializeObject(br);

        }
    }
}
