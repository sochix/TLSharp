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

        public int pos { get; set; }
        public int user_id { get; set; }
        public int score { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            pos = br.ReadInt32();
            user_id = br.ReadInt32();
            score = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(pos);
            bw.Write(user_id);
            bw.Write(score);

        }
    }
}
