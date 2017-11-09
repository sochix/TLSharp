using System.IO;
namespace TeleSharp.TL
{
    [TLObject(-648121413)]
    public class TLInputMessagesFilterPhotoVideoDocuments : TLAbsMessagesFilter
    {
        public override int Constructor
        {
            get
            {
                return -648121413;
            }
        }



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
    }
}
