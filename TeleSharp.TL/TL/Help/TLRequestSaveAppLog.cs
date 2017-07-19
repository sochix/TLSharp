using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Help
{
    [TLObject(1862465352)]
    public class TLRequestSaveAppLog : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1862465352;
            }
        }

        public TLVector<TLInputAppEvent> events { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            events = (TLVector<TLInputAppEvent>)ObjectUtils.DeserializeVector<TLInputAppEvent>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(events, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
