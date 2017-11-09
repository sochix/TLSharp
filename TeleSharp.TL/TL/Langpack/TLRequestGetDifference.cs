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

        public int from_version { get; set; }
        public TLLangPackDifference Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            from_version = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(from_version);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLLangPackDifference)ObjectUtils.DeserializeObject(br);

        }
    }
}
