using System.IO;
using TeleSharp.TL.Messages;

namespace TeleSharp.TL.Channels
{
    [TLObject(-1920105769)]
    public class TLRequestGetAdminedPublicChannels : TLMethod
    {
        public override int Constructor => -1920105769;

        public TLChats Response { get; set; }


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
            Response = (TLChats) ObjectUtils.DeserializeObject(br);
        }
    }
}