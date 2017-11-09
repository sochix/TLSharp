using System.IO;
namespace TeleSharp.TL
{
    [TLObject(-627372787)]
    public class TLRequestInvokeWithLayer : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -627372787;
            }
        }

        public int layer { get; set; }
        public TLObject query { get; set; }
        public TLObject Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            layer = br.ReadInt32();
            query = (TLObject)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(layer);
            ObjectUtils.SerializeObject(query, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLObject)ObjectUtils.DeserializeObject(br);

        }
    }
}
