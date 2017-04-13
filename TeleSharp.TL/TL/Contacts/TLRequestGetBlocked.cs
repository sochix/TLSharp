using System.IO;

namespace TeleSharp.TL.Contacts
{
    [TLObject(-176409329)]
    public class TLRequestGetBlocked : TLMethod
    {
        public override int Constructor => -176409329;

        public int offset { get; set; }
        public int limit { get; set; }
        public TLAbsBlocked Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            offset = br.ReadInt32();
            limit = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(offset);
            bw.Write(limit);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsBlocked) ObjectUtils.DeserializeObject(br);
        }
    }
}