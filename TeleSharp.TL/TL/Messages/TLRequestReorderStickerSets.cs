using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(2016638777)]
    public class TLRequestReorderStickerSets : TLMethod
    {
        public override int Constructor => 2016638777;

        public int flags { get; set; }
        public bool masks { get; set; }
        public TLVector<long> order { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = masks ? flags | 1 : flags & ~1;
        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            masks = (flags & 1) != 0;
            order = ObjectUtils.DeserializeVector<long>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);

            ObjectUtils.SerializeObject(order, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }
    }
}