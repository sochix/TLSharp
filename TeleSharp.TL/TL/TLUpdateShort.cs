using System.IO;

namespace TeleSharp.TL
{
    [TLObject(2027216577)]
    public class TLUpdateShort : TLAbsUpdates
    {
        public override int Constructor => 2027216577;

        public TLAbsUpdate update { get; set; }
        public int date { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            update = (TLAbsUpdate) ObjectUtils.DeserializeObject(br);
            date = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(update, bw);
            bw.Write(date);
        }
    }
}