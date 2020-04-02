using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1996904104)]
    public class TLInputAppEvent : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 1996904104;
            }
        }

        public double Time { get; set; }
        public string Type { get; set; }
        public long Peer { get; set; }
        public string Data { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Time = br.ReadDouble();
            Type = StringUtil.Deserialize(br);
            Peer = br.ReadInt64();
            Data = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Time);
            StringUtil.Serialize(Type, bw);
            bw.Write(Peer);
            StringUtil.Serialize(Data, bw);

        }
    }
}
