using System.IO;

namespace TeleSharp.TL.Help
{
    [TLObject(1295590211)]
    public class TLRequestGetInviteText : TLMethod
    {
        public override int Constructor => 1295590211;

        public TLInviteText Response { get; set; }


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
            Response = (TLInviteText) ObjectUtils.DeserializeObject(br);
        }
    }
}