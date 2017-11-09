using System.IO;
namespace TeleSharp.TL.Help
{
    [TLObject(-990308245)]
    public class TLRequestGetConfig : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -990308245;
            }
        }

        public TLConfig Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLConfig)ObjectUtils.DeserializeObject(br);

        }
    }
}
