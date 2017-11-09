using System.IO;
namespace TeleSharp.TL
{
    [TLObject(-1987495099)]
    public class TLUpdateChannelReadMessagesContents : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -1987495099;
            }
        }

        public int channel_id { get; set; }
        public TLVector<int> messages { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            channel_id = br.ReadInt32();
            messages = (TLVector<int>)ObjectUtils.DeserializeVector<int>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(channel_id);
            ObjectUtils.SerializeObject(messages, bw);

        }
    }
}
