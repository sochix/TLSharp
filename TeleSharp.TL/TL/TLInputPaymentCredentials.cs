using System.IO;

namespace TeleSharp.TL
{
    [TLObject(873977640)]
    public class TLInputPaymentCredentials : TLAbsInputPaymentCredentials
    {
        public override int Constructor
        {
            get
            {
                return 873977640;
            }
        }

        public TLDataJSON Data { get; set; }

        public int Flags { get; set; }

        public bool Save { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Save = (Flags & 1) != 0;
            Data = (TLDataJSON)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);

            ObjectUtils.SerializeObject(Data, bw);
        }
    }
}
