using System.IO;
namespace TeleSharp.TL.Messages
{
    [TLObject(-1707344487)]
    public class TLHighScores : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1707344487;
            }
        }

        public TLVector<TLHighScore> scores { get; set; }
        public TLVector<TLAbsUser> users { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            scores = (TLVector<TLHighScore>)ObjectUtils.DeserializeVector<TLHighScore>(br);
            users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(scores, bw);
            ObjectUtils.SerializeObject(users, bw);

        }
    }
}
