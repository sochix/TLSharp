using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1036301552)]
    public class TLRequestInvokeAfterMsgs : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1036301552;
            }
        }

        public TLVector<long> msg_ids { get; set; }
        public TLObject query { get; set; }
        public TLObject Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            msg_ids = (TLVector<long>)ObjectUtils.DeserializeVector<long>(br);
            query = (TLObject)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(msg_ids, bw);
            ObjectUtils.SerializeObject(query, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLObject)ObjectUtils.DeserializeObject(br);

        }
    }
}
