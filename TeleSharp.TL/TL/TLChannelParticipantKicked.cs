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

        public int user_id { get; set; }
        public int kicked_by { get; set; }
        public int date { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            user_id = br.ReadInt32();
            kicked_by = br.ReadInt32();
            date = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(user_id);
            bw.Write(kicked_by);
            bw.Write(date);

        }
    }
}
