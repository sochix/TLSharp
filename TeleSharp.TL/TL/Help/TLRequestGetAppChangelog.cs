using System.IO;

namespace TeleSharp.TL.Help
{
    [TLObject(-1877938321)]
    public class TLRequestGetAppChangelog : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1877938321;
            }
        }

        public string PrevAppVersion { get; set; }

        public TLAbsUpdates Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            PrevAppVersion = StringUtil.Deserialize(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(PrevAppVersion, bw);
        }
    }
}
