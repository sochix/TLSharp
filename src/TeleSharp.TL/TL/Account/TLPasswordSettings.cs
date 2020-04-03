using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(-1705233435)]
    public class TLPasswordSettings : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1705233435;
            }
        }

        public int Flags { get; set; }
        public string Email { get; set; }
        public TLSecureSecretSettings SecureSettings { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            if ((Flags & 1) != 0)
                Email = StringUtil.Deserialize(br);
            else
                Email = null;

            if ((Flags & 2) != 0)
                SecureSettings = (TLSecureSecretSettings)ObjectUtils.DeserializeObject(br);
            else
                SecureSettings = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            if ((Flags & 1) != 0)
                StringUtil.Serialize(Email, bw);
            if ((Flags & 2) != 0)
                ObjectUtils.SerializeObject(SecureSettings, bw);

        }
    }
}
