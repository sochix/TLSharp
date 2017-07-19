using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(1436924774)]
    public class TLRequestReceivedQueue : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1436924774;
            }
        }

        public int max_qts { get; set; }
        public TLVector<long> Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            max_qts = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(max_qts);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLVector<long>)ObjectUtils.DeserializeVector<long>(br);

        }
    }
}
