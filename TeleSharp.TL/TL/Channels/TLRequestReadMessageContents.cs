using System.IO;
namespace TeleSharp.TL.Channels
{
    [TLObject(-357180360)]
    public class TLRequestReadMessageContents : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -357180360;
            }
        }

        public TLAbsInputChannel channel { get; set; }
        public TLVector<int> id { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
            id = (TLVector<int>)ObjectUtils.DeserializeVector<int>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(channel, bw);
            ObjectUtils.SerializeObject(id, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
