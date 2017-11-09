using System.IO;
namespace TeleSharp.TL.Contacts
{
    [TLObject(-2020263951)]
    public class TLRequestResetSaved : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -2020263951;
            }
        }

        public bool Response { get; set; }


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
            Response = BoolUtil.Deserialize(br);

        }
    }
}
