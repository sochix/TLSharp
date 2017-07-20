using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1834538890)]
    public class TLMessageActionGameScore : TLAbsMessageAction
    {
        public override int Constructor
        {
            get
            {
                return -1834538890;
            }
        }

        public long game_id { get; set; }
        public int score { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            game_id = br.ReadInt64();
            score = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(game_id);
            bw.Write(score);

        }
    }
}
