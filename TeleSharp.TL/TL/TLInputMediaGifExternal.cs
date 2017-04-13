using System.IO;

namespace TeleSharp.TL
{
    [TLObject(1212395773)]
    public class TLInputMediaGifExternal : TLAbsInputMedia
    {
        public override int Constructor => 1212395773;

        public string url { get; set; }
        public string q { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            url = StringUtil.Deserialize(br);
            q = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(url, bw);
            StringUtil.Serialize(q, bw);
        }
    }
}