using System.IO;

namespace TeleSharp.TL
{
    [TLObject(2104790276)]
    public class TLDataJSON : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 2104790276;
            }
        }

        public string Data { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Data = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Data, bw);
        }
    }
}
