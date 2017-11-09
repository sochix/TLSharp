using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1933187430)]
    public class TLChannelParticipantKicked : TLAbsChannelParticipant
    {
        public override int Constructor
        {
            get
            {
                return -1933187430;
            }
        }

        public int UserId { get; set; }
        public int KickedBy { get; set; }
        public int Date { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            UserId = br.ReadInt32();
            KickedBy = br.ReadInt32();
            Date = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(UserId);
            bw.Write(KickedBy);
            bw.Write(Date);

        }
    }
}
