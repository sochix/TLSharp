using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(1158290442)]
    public class TLFoundGifs : TLObject
    {
        public override int Constructor => 1158290442;

        public int next_offset { get; set; }
        public TLVector<TLAbsFoundGif> results { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            next_offset = br.ReadInt32();
            results = ObjectUtils.DeserializeVector<TLAbsFoundGif>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(next_offset);
            ObjectUtils.SerializeObject(results, bw);
        }
    }
}