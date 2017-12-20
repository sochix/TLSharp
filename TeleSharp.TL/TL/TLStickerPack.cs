using System.IO;

namespace TeleSharp.TL
{
    [TLObject(313694676)]
    public class TLStickerPack : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 313694676;
            }
        }

        public TLVector<long> Documents { get; set; }

        public string Emoticon { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Emoticon = StringUtil.Deserialize(br);
            Documents = (TLVector<long>)ObjectUtils.DeserializeVector<long>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Emoticon, bw);
            ObjectUtils.SerializeObject(Documents, bw);
        }
    }
}
