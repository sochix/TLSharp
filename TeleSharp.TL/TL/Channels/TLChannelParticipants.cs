using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Channels
{
    [TLObject(-177282392)]
    public class TLChannelParticipants : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -177282392;
            }
        }

        public int count { get; set; }
        public TLVector<TLAbsChannelParticipant> participants { get; set; }
        public TLVector<TLAbsUser> users { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            count = br.ReadInt32();
            participants = (TLVector<TLAbsChannelParticipant>)ObjectUtils.DeserializeVector<TLAbsChannelParticipant>(br);
            users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(count);
            ObjectUtils.SerializeObject(participants, bw);
            ObjectUtils.SerializeObject(users, bw);

        }
    }
}
