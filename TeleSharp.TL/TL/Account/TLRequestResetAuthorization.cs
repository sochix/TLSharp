using System.IO;

namespace TeleSharp.TL.Account
{
    [TLObject(-545786948)]
    public class TLRequestResetAuthorization : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -545786948;
            }
        }

        public long Hash { get; set; }

        public bool Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Hash = br.ReadInt64();
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Hash);
        }
    }
}
