using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1996904104)]
    public class TLInputAppEvent : TLObject
    {
        public override int Constructor => 1996904104;

        public double time { get; set; }
        public string type { get; set; }
        public long peer { get; set; }
        public string data { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            time = br.ReadDouble();
            type = StringUtil.Deserialize(br);
            peer = br.ReadInt64();
            data = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(time);
            StringUtil.Serialize(type, bw);
            bw.Write(peer);
            StringUtil.Serialize(data, bw);
        }
    }
}