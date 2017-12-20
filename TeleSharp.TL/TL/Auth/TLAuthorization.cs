using System.IO;

namespace TeleSharp.TL.Auth
{
    [TLObject(-855308010)]
    public class TLAuthorization : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -855308010;
            }
        }

        public int Flags { get; set; }

        public int? TmpSessions { get; set; }

        public TLAbsUser User { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            if ((Flags & 1) != 0)
                TmpSessions = br.ReadInt32();
            else
                TmpSessions = null;

            User = (TLAbsUser)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            if ((Flags & 1) != 0)
                bw.Write(TmpSessions.Value);
            ObjectUtils.SerializeObject(User, bw);
        }
    }
}
