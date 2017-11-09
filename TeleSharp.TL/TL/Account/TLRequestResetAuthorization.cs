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

        public long hash { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            hash = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(hash);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
