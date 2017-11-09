using System.IO;
namespace TeleSharp.TL
{
    [TLObject(1399245077)]
    public class TLPhoneCallEmpty : TLAbsPhoneCall
    {
        public override int Constructor
        {
            get
            {
                return 1399245077;
            }
        }

        public long id { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(id);

        }
    }
}
