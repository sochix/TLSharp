using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(-2084618926)]
    public class TLRequestGetSavedGifs : TLMethod
    {
        public override int Constructor => -2084618926;

        public int hash { get; set; }
        public TLAbsSavedGifs Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            hash = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(hash);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsSavedGifs) ObjectUtils.DeserializeObject(br);
        }
    }
}