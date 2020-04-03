using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1398708869)]
    public class TLUpdateMessagePoll : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -1398708869;
            }
        }

        public int Flags { get; set; }
        public long PollId { get; set; }
        public TLPoll Poll { get; set; }
        public TLPollResults Results { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            PollId = br.ReadInt64();
            if ((Flags & 1) != 0)
                Poll = (TLPoll)ObjectUtils.DeserializeObject(br);
            else
                Poll = null;

            Results = (TLPollResults)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            bw.Write(PollId);
            if ((Flags & 1) != 0)
                ObjectUtils.SerializeObject(Poll, bw);
            ObjectUtils.SerializeObject(Results, bw);

        }
    }
}
