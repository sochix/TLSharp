using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(-1080395925)]
    public class TLRequestSearchGifs : TLMethod
    {
        public override int Constructor => -1080395925;

        public string q { get; set; }
        public int offset { get; set; }
        public TLFoundGifs Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            q = StringUtil.Deserialize(br);
            offset = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(q, bw);
            bw.Write(offset);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLFoundGifs) ObjectUtils.DeserializeObject(br);
        }
    }
}