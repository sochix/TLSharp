using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(-1510897371)]
    public class TLRequestDeleteMessages : TLMethod
    {
        public override int Constructor => -1510897371;

        public TLVector<int> id { get; set; }
        public TLAffectedMessages Response { get; set; }


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
            Response = (TLAffectedMessages) ObjectUtils.DeserializeObject(br);
        }
    }
}