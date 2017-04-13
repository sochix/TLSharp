using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(1109588596)]
    public class TLRequestGetMessages : TLMethod
    {
        public override int Constructor => 1109588596;

        public TLVector<int> id { get; set; }
        public TLAbsMessages Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = ObjectUtils.DeserializeVector<int>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(id, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsMessages) ObjectUtils.DeserializeObject(br);
        }
    }
}