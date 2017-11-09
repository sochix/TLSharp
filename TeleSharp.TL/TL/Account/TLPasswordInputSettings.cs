using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(-2037289493)]
    public class TLPasswordInputSettings : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -2037289493;
            }
        }

        public int Flags { get; set; }
        public byte[] NewSalt { get; set; }
        public byte[] NewPasswordHash { get; set; }
        public string Hint { get; set; }
        public string Email { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = NewSalt != null ? (Flags | 1) : (Flags & ~1);
            Flags = NewPasswordHash != null ? (Flags | 1) : (Flags & ~1);
            Flags = Hint != null ? (Flags | 1) : (Flags & ~1);
            Flags = Email != null ? (Flags | 2) : (Flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            if ((Flags & 1) != 0)
                NewSalt = BytesUtil.Deserialize(br);
            else
                NewSalt = null;

            if ((Flags & 1) != 0)
                NewPasswordHash = BytesUtil.Deserialize(br);
            else
                NewPasswordHash = null;

            if ((Flags & 1) != 0)
                Hint = StringUtil.Deserialize(br);
            else
                Hint = null;

            if ((Flags & 2) != 0)
                Email = StringUtil.Deserialize(br);
            else
                Email = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);
            if ((Flags & 1) != 0)
                BytesUtil.Serialize(NewSalt, bw);
            if ((Flags & 1) != 0)
                BytesUtil.Serialize(NewPasswordHash, bw);
            if ((Flags & 1) != 0)
                StringUtil.Serialize(Hint, bw);
            if ((Flags & 2) != 0)
                StringUtil.Serialize(Email, bw);

        }
    }
}
