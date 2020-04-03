using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(-1389486888)]
    public class TLAuthorizationForm : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1389486888;
            }
        }

        public int Flags { get; set; }
        public TLVector<TLAbsSecureRequiredType> RequiredTypes { get; set; }
        public TLVector<TLSecureValue> Values { get; set; }
        public TLVector<TLAbsSecureValueError> Errors { get; set; }
        public TLVector<TLAbsUser> Users { get; set; }
        public string PrivacyPolicyUrl { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            RequiredTypes = (TLVector<TLAbsSecureRequiredType>)ObjectUtils.DeserializeVector<TLAbsSecureRequiredType>(br);
            Values = (TLVector<TLSecureValue>)ObjectUtils.DeserializeVector<TLSecureValue>(br);
            Errors = (TLVector<TLAbsSecureValueError>)ObjectUtils.DeserializeVector<TLAbsSecureValueError>(br);
            Users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);
            if ((Flags & 1) != 0)
                PrivacyPolicyUrl = StringUtil.Deserialize(br);
            else
                PrivacyPolicyUrl = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            ObjectUtils.SerializeObject(RequiredTypes, bw);
            ObjectUtils.SerializeObject(Values, bw);
            ObjectUtils.SerializeObject(Errors, bw);
            ObjectUtils.SerializeObject(Users, bw);
            if ((Flags & 1) != 0)
                StringUtil.Serialize(PrivacyPolicyUrl, bw);

        }
    }
}
