using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(70813275)]
    public class TLInputStickeredMediaDocument : TLAbsInputStickeredMedia
    {
        public override int Constructor
        {
            get
            {
                return 70813275;
            }
        }

        public TLAbsInputDocument Id { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Id = (TLAbsInputDocument)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Id, bw);

        }
    }
}
