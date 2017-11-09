using System.IO;
namespace TeleSharp.TL.Contacts
{
    [TLObject(-2065352905)]
    public class TLRequestExportCard : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -2065352905;
            }
        }

        public TLVector<int> Response { get; set; }


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
            Response = (TLVector<int>)ObjectUtils.DeserializeVector<int>(br);

        }
    }
}
