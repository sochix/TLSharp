using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(-1390001672)]
    public class TLPassword : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1390001672;
            }
        }

        public int Flags { get; set; }
        public bool HasRecovery { get; set; }
        public bool HasSecureValues { get; set; }
        public bool HasPassword { get; set; }
        public TLAbsPasswordKdfAlgo CurrentAlgo { get; set; }
        public byte[] SrpB { get; set; }
        public long? SrpId { get; set; }
        public string Hint { get; set; }
        public string EmailUnconfirmedPattern { get; set; }
        public TLAbsPasswordKdfAlgo NewAlgo { get; set; }
        public TLAbsSecurePasswordKdfAlgo NewSecureAlgo { get; set; }
        public byte[] SecureRandom { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            HasRecovery = (Flags & 1) != 0;
            HasSecureValues = (Flags & 2) != 0;
            HasPassword = (Flags & 4) != 0;
            if ((Flags & 4) != 0)
                CurrentAlgo = (TLAbsPasswordKdfAlgo)ObjectUtils.DeserializeObject(br);
            else
                CurrentAlgo = null;

            if ((Flags & 4) != 0)
                SrpB = BytesUtil.Deserialize(br);
            else
                SrpB = null;

            if ((Flags & 4) != 0)
                SrpId = br.ReadInt64();
            else
                SrpId = null;

            if ((Flags & 8) != 0)
                Hint = StringUtil.Deserialize(br);
            else
                Hint = null;

            if ((Flags & 16) != 0)
                EmailUnconfirmedPattern = StringUtil.Deserialize(br);
            else
                EmailUnconfirmedPattern = null;

            NewAlgo = (TLAbsPasswordKdfAlgo)ObjectUtils.DeserializeObject(br);
            NewSecureAlgo = (TLAbsSecurePasswordKdfAlgo)ObjectUtils.DeserializeObject(br);
            SecureRandom = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);



            if ((Flags & 4) != 0)
                ObjectUtils.SerializeObject(CurrentAlgo, bw);
            if ((Flags & 4) != 0)
                BytesUtil.Serialize(SrpB, bw);
            if ((Flags & 4) != 0)
                bw.Write(SrpId.Value);
            if ((Flags & 8) != 0)
                StringUtil.Serialize(Hint, bw);
            if ((Flags & 16) != 0)
                StringUtil.Serialize(EmailUnconfirmedPattern, bw);
            ObjectUtils.SerializeObject(NewAlgo, bw);
            ObjectUtils.SerializeObject(NewSecureAlgo, bw);
            BytesUtil.Serialize(SecureRandom, bw);

        }
    }
}
