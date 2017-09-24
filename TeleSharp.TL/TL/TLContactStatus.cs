using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-748155807)]
    public class TLContactStatus : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -748155807;
            }
        }

        public int user_id { get; set; }
        public TLAbsUserStatus status { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            user_id = br.ReadInt32();
            status = (TLAbsUserStatus)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(user_id);
            ObjectUtils.SerializeObject(status, bw);
        }
    }
}