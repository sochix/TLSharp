using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1906403213)]
    public class TLUpdateDcOptions : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -1906403213;
            }
        }

        public TLVector<TLDcOption> DcOptions { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            DcOptions = (TLVector<TLDcOption>)ObjectUtils.DeserializeVector<TLDcOption>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(DcOptions, bw);
        }
    }
}
