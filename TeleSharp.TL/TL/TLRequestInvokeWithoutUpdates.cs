using System.IO;
namespace TeleSharp.TL
{
    [TLObject(-1080796745)]
    public class TLRequestInvokeWithoutUpdates : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1080796745;
            }
        }

        public TLObject query { get; set; }
        public TLObject Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            query = (TLObject)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(query, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLObject)ObjectUtils.DeserializeObject(br);

        }
    }
}
