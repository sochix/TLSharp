using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(852135825)]
    public class TLRequestGetWebPage : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 852135825;
            }
        }

        public int Hash { get; set; }

        public TLAbsWebPage Response { get; set; }

        public string Url { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Url = StringUtil.Deserialize(br);
            Hash = br.ReadInt32();
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLAbsWebPage)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Url, bw);
            bw.Write(Hash);
        }
    }
}
