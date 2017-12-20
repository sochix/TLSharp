using System.IO;

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

        public TLDataJSON Debug { get; set; }

        public TLInputPhoneCall Peer { get; set; }

        public bool Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Peer = (TLInputPhoneCall)ObjectUtils.DeserializeObject(br);
            Debug = (TLDataJSON)ObjectUtils.DeserializeObject(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Peer, bw);
            ObjectUtils.SerializeObject(Debug, bw);
        }
    }
}
