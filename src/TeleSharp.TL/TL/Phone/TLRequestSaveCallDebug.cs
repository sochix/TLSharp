using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Phone
{
    [TLObject(662363518)]
    public class TLRequestSaveCallDebug : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 662363518;
            }
        }

        public TLInputPhoneCall Peer { get; set; }
        public TLDataJSON Debug { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Peer = (TLInputPhoneCall)ObjectUtils.DeserializeObject(br);
            Debug = (TLDataJSON)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Peer, bw);
            ObjectUtils.SerializeObject(Debug, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
