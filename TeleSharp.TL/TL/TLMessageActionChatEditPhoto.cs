using System.IO;

namespace TeleSharp.TL
{
    [TLObject(2144015272)]
    public class TLMessageActionChatEditPhoto : TLAbsMessageAction
    {
        public override int Constructor
        {
            get
            {
                return 2144015272;
            }
        }

        public TLAbsPhoto photo { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            photo = (TLAbsPhoto)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(photo, bw);
        }
    }
}