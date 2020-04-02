using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(125178264)]
    public class TLUpdateChatParticipants : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 125178264;
            }
        }

        public TLAbsChatParticipants Participants { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Participants = (TLAbsChatParticipants)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Participants, bw);

        }
    }
}
