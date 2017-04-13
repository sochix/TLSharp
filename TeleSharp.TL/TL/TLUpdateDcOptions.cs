using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1906403213)]
    public class TLUpdateDcOptions : TLAbsUpdate
    {
        public override int Constructor => -1906403213;

        public TLVector<TLDcOption> dc_options { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            dc_options = ObjectUtils.DeserializeVector<TLDcOption>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(dc_options, bw);
        }
    }
}