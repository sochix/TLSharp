using System.IO;

namespace TeleSharp.TL.Payments
{
    [TLObject(-667062079)]
    public class TLRequestClearSavedInfo : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -667062079;
            }
        }

        public bool Credentials { get; set; }

        public int Flags { get; set; }

        public bool Info { get; set; }

        public bool Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Credentials = (Flags & 1) != 0;
            Info = (Flags & 2) != 0;
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
        }
    }
}
