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

        public int flags { get; set; }
        public byte[] new_salt { get; set; }
        public byte[] new_password_hash { get; set; }
        public string hint { get; set; }
        public string email { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = new_salt != null ? (flags | 1) : (flags & ~1);
            flags = new_password_hash != null ? (flags | 1) : (flags & ~1);
            flags = hint != null ? (flags | 1) : (flags & ~1);
            flags = email != null ? (flags | 2) : (flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            if ((flags & 1) != 0)
                new_salt = BytesUtil.Deserialize(br);
            else
                new_salt = null;

            if ((flags & 1) != 0)
                new_password_hash = BytesUtil.Deserialize(br);
            else
                new_password_hash = null;

            if ((flags & 1) != 0)
                hint = StringUtil.Deserialize(br);
            else
                hint = null;

            if ((flags & 2) != 0)
                email = StringUtil.Deserialize(br);
            else
                email = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);
            if ((flags & 1) != 0)
                BytesUtil.Serialize(new_salt, bw);
            if ((flags & 1) != 0)
                BytesUtil.Serialize(new_password_hash, bw);
            if ((flags & 1) != 0)
                StringUtil.Serialize(hint, bw);
            if ((flags & 2) != 0)
                StringUtil.Serialize(email, bw);

        }
    }
}
