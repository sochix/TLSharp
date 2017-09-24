using System.IO;

namespace TeleSharp.TL.Channels
{
    [TLObject(176122811)]
    public class TLRequestGetChannels : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 176122811;
            }
        }

        public TLVector<TLAbsInputChannel> id { get; set; }
        public Messages.TLAbsChats Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = (TLVector<TLAbsInputChannel>)ObjectUtils.DeserializeVector<TLAbsInputChannel>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(id, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLAbsChats)ObjectUtils.DeserializeObject(br);
        }
    }
}