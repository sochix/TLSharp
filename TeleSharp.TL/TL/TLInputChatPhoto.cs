using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1293828344)]
    public class TLInputChatPhoto : TLAbsInputChatPhoto
    {
        public override int Constructor
        {
            get
            {
                return -1293828344;
            }
        }

        public TLAbsInputPhoto id { get; set; }
        public TLAbsInputPhotoCrop crop { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = (TLAbsInputPhoto)ObjectUtils.DeserializeObject(br);
            crop = (TLAbsInputPhotoCrop)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(id, bw);
            ObjectUtils.SerializeObject(crop, bw);

        }
    }
}
