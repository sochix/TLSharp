using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(-419267436)]
    public class TLRequestAcceptAuthorization : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -419267436;
            }
        }

        public int BotId { get; set; }
        public string Scope { get; set; }
        public string PublicKey { get; set; }
        public TLVector<TLSecureValueHash> ValueHashes { get; set; }
        public TLSecureCredentialsEncrypted Credentials { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            BotId = br.ReadInt32();
            Scope = StringUtil.Deserialize(br);
            PublicKey = StringUtil.Deserialize(br);
            ValueHashes = (TLVector<TLSecureValueHash>)ObjectUtils.DeserializeVector<TLSecureValueHash>(br);
            Credentials = (TLSecureCredentialsEncrypted)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(BotId);
            StringUtil.Serialize(Scope, bw);
            StringUtil.Serialize(PublicKey, bw);
            ObjectUtils.SerializeObject(ValueHashes, bw);
            ObjectUtils.SerializeObject(Credentials, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
