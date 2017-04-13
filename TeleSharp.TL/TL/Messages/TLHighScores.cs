using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(-1707344487)]
    public class TLHighScores : TLObject
    {
        public override int Constructor => -1707344487;

        public TLVector<TLHighScore> scores { get; set; }
        public TLVector<TLAbsUser> users { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            scores = ObjectUtils.DeserializeVector<TLHighScore>(br);
            users = ObjectUtils.DeserializeVector<TLAbsUser>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(scores, bw);
            ObjectUtils.SerializeObject(users, bw);
        }
    }
}