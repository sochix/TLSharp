using System.IO;

namespace TeleSharp.TL
{
    [TLObject(628472761)]
    public class TLUpdateContactRegistered : TLAbsUpdate
    {
        public override int Constructor => 628472761;

        public int user_id { get; set; }
        public int date { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            user_id = br.ReadInt32();
            date = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(user_id);
            bw.Write(date);
        }
    }
}