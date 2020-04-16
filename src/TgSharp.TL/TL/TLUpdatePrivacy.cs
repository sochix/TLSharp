using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL
{
    [TLObject(-298113238)]
    public class TLUpdatePrivacy : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -298113238;
            }
        }

        public TLAbsPrivacyKey Key { get; set; }
        public TLVector<TLAbsPrivacyRule> Rules { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Key = (TLAbsPrivacyKey)ObjectUtils.DeserializeObject(br);
            Rules = (TLVector<TLAbsPrivacyRule>)ObjectUtils.DeserializeVector<TLAbsPrivacyRule>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Key, bw);
            ObjectUtils.SerializeObject(Rules, bw);
        }
    }
}
