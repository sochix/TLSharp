using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1036396922)]
    public class TLInputWebFileLocation : TLObject
    {
        public long AccessHash { get; set; }

        public override int Constructor
        {
            get
            {
                return -1036396922;
            }
        }

        public string Url { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Url = StringUtil.Deserialize(br);
            AccessHash = br.ReadInt64();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Url, bw);
            bw.Write(AccessHash);
        }
    }
}
