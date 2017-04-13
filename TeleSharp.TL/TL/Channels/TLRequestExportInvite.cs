using System.IO;

namespace TeleSharp.TL.Channels
{
    [TLObject(-950663035)]
    public class TLRequestExportInvite : TLMethod
    {
        public override int Constructor => -950663035;

        public TLAbsInputChannel channel { get; set; }
        public TLAbsExportedChatInvite Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            channel = (TLAbsInputChannel) ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(channel, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsExportedChatInvite) ObjectUtils.DeserializeObject(br);
        }
    }
}