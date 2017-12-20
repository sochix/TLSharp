using System.IO;

namespace TeleSharp.TL.Account
{
    [TLObject(1707432768)]
    public class TLRequestUnregisterDevice : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1707432768;
            }
        }

        public bool Response { get; set; }

        public string Token { get; set; }

        public int TokenType { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            TokenType = br.ReadInt32();
            Token = StringUtil.Deserialize(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(TokenType);
            StringUtil.Serialize(Token, bw);
        }
    }
}
