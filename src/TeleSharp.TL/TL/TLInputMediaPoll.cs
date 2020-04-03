using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(112424539)]
    public class TLInputMediaPoll : TLAbsInputMedia
    {
        public override int Constructor
        {
            get
            {
                return 112424539;
            }
        }

        public TLPoll Poll { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Poll = (TLPoll)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Poll, bw);

        }
    }
}
