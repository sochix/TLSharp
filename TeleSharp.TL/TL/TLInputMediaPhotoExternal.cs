using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1252045032)]
    public class TLInputMediaPhotoExternal : TLAbsInputMedia
    {
        public override int Constructor => -1252045032;

        public string url { get; set; }
        public string caption { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            url = StringUtil.Deserialize(br);
            caption = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(url, bw);
            StringUtil.Serialize(caption, bw);
        }
    }
}