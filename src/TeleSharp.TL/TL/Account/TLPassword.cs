using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(2081952796)]
    public class TLPassword : TLAbsPassword
    {
        public override int Constructor
        {
            get
            {
                return 2081952796;
            }
        }

        public byte[] CurrentSalt { get; set; }
        public byte[] NewSalt { get; set; }
        public string Hint { get; set; }
        public bool HasRecovery { get; set; }
        public string EmailUnconfirmedPattern { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            CurrentSalt = BytesUtil.Deserialize(br);
            NewSalt = BytesUtil.Deserialize(br);
            Hint = StringUtil.Deserialize(br);
            HasRecovery = BoolUtil.Deserialize(br);
            EmailUnconfirmedPattern = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            BytesUtil.Serialize(CurrentSalt, bw);
            BytesUtil.Serialize(NewSalt, bw);
            StringUtil.Serialize(Hint, bw);
            BoolUtil.Serialize(HasRecovery, bw);
            StringUtil.Serialize(EmailUnconfirmedPattern, bw);

        }
    }
}
