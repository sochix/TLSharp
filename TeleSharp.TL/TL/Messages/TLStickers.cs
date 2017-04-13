using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(-1970352846)]
    public class TLStickers : TLAbsStickers
    {
        public override int Constructor => -1970352846;

        public string hash { get; set; }
        public TLVector<TLAbsDocument> stickers { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            hash = StringUtil.Deserialize(br);
            stickers = ObjectUtils.DeserializeVector<TLAbsDocument>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(hash, bw);
            ObjectUtils.SerializeObject(stickers, bw);
        }
    }
}