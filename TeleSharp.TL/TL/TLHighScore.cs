using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1493171408)]
    public class TLHighScore : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 1493171408;
            }
        }

        public int Pos { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Pos = br.ReadInt32();
            UserId = br.ReadInt32();
            Score = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Pos);
            bw.Write(UserId);
            bw.Write(Score);

        }
    }
}
