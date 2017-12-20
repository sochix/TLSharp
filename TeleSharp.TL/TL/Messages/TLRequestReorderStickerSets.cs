using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(2016638777)]
    public class TLRequestReorderStickerSets : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 2016638777;
            }
        }

        public int Flags { get; set; }

        public bool Masks { get; set; }

        public TLVector<long> Order { get; set; }

        public bool Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Masks = (Flags & 1) != 0;
            Order = (TLVector<long>)ObjectUtils.DeserializeVector<long>(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);

            ObjectUtils.SerializeObject(Order, bw);
        }
    }
}
