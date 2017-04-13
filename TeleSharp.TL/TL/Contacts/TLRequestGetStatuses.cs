using System.IO;

namespace TeleSharp.TL.Contacts
{
    [TLObject(-995929106)]
    public class TLRequestGetStatuses : TLMethod
    {
        public override int Constructor => -995929106;

        public TLVector<TLContactStatus> Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = ObjectUtils.DeserializeVector<TLContactStatus>(br);
        }
    }
}