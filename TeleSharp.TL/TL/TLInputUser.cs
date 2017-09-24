using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-668391402)]
    public class TLInputUser : TLAbsInputUser
    {
        public override int Constructor
        {
            get
            {
                return -668391402;
            }
        }

        public int user_id { get; set; }
        public long access_hash { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            user_id = br.ReadInt32();
            access_hash = br.ReadInt64();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(user_id);
            bw.Write(access_hash);
        }
    }
}