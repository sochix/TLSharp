using System.IO;

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

        public string Email { get; set; }

        public int Flags { get; set; }

        public string Hint { get; set; }

        public byte[] NewPasswordHash { get; set; }

        public byte[] NewSalt { get; set; }

        public void ComputeFlags()
        {
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
