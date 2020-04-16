using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL
{
    [TLObject(-321430132)]
    public class TLUserProfilePhoto : TLAbsUserProfilePhoto
    {
        public override int Constructor
        {
            get
            {
                return -321430132;
            }
        }

        public long PhotoId { get; set; }
        // manual edit: FileLocation->TLFileLocationToBeDeprecated
        public TLFileLocationToBeDeprecated PhotoSmall { get; set; }
        // manual edit: FileLocation->TLFileLocationToBeDeprecated
        public TLFileLocationToBeDeprecated PhotoBig { get; set; }
        public int DcId { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            PhotoId = br.ReadInt64();
            // manual edit: FileLocation->TLFileLocationToBeDeprecated
            PhotoSmall = (TLFileLocationToBeDeprecated)ObjectUtils.DeserializeObject(br);
            // manual edit: FileLocation->TLFileLocationToBeDeprecated
            PhotoBig = (TLFileLocationToBeDeprecated)ObjectUtils.DeserializeObject(br);
            DcId = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(PhotoId);
            ObjectUtils.SerializeObject(PhotoSmall, bw);
            ObjectUtils.SerializeObject(PhotoBig, bw);
            bw.Write(DcId);
        }
    }
}
