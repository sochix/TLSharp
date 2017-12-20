using System.IO;

namespace TeleSharp.TL.Bots
{
    [TLObject(-1440257555)]
    public class TLRequestSendCustomRequest : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1440257555;
            }
        }

        public string CustomMethod { get; set; }

        public TLDataJSON Params { get; set; }

        public TLDataJSON Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            CustomMethod = StringUtil.Deserialize(br);
            Params = (TLDataJSON)ObjectUtils.DeserializeObject(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLDataJSON)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(CustomMethod, bw);
            ObjectUtils.SerializeObject(Params, bw);
        }
    }
}
