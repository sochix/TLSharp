using System.IO;

namespace TeleSharp.TL.Langpack
{
    [TLObject(187583869)]
    public class TLRequestGetDifference : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 187583869;
            }
        }

        public int FromVersion { get; set; }

        public TLLangPackDifference Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            FromVersion = br.ReadInt32();
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLLangPackDifference)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(FromVersion);
        }
    }
}
