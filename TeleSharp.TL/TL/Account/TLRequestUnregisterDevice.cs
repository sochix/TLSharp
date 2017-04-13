using System.IO;

namespace TeleSharp.TL.Account
{
    [TLObject(1707432768)]
    public class TLRequestUnregisterDevice : TLMethod
    {
        public override int Constructor => 1707432768;

        public int token_type { get; set; }
        public string token { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            token_type = br.ReadInt32();
            token = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(token_type);
            StringUtil.Serialize(token, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }
    }
}