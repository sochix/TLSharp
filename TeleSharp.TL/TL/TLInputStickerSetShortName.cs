using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-2044933984)]
    public class TLInputStickerSetShortName : TLAbsInputStickerSet
    {
        public override int Constructor => -2044933984;

        public string short_name { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            short_name = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(short_name, bw);
        }
    }
}