using System.IO;
namespace TeleSharp.TL
{
    [TLObject(-116274796)]
    public class TLContact : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -116274796;
            }
        }

        public int user_id { get; set; }
        public bool mutual { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            user_id = br.ReadInt32();
            mutual = BoolUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(user_id);
            BoolUtil.Serialize(mutual, bw);

        }
    }
}
