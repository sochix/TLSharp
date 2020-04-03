using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(997055186)]
    public class TLPollAnswerVoters : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 997055186;
            }
        }

        public int Flags { get; set; }
        public bool Chosen { get; set; }
        public bool Correct { get; set; }
        public byte[] Option { get; set; }
        public int Voters { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Chosen = (Flags & 1) != 0;
            Correct = (Flags & 2) != 0;
            Option = BytesUtil.Deserialize(br);
            Voters = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);


            BytesUtil.Serialize(Option, bw);
            bw.Write(Voters);

        }
    }
}
