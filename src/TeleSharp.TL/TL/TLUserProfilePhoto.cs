using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-715532088)]
    public class TLUserProfilePhoto : TLAbsUserProfilePhoto
    {
        public override int Constructor
        {
            get
            {
                return -715532088;
            }
        }

        public long PhotoId { get; set; }
        public TLAbsFileLocation PhotoSmall { get; set; }
        public TLAbsFileLocation PhotoBig { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            PhotoId = br.ReadInt64();
            PhotoSmall = (TLAbsFileLocation)ObjectUtils.DeserializeObject(br);
            PhotoBig = (TLAbsFileLocation)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(PhotoId);
            ObjectUtils.SerializeObject(PhotoSmall, bw);
            ObjectUtils.SerializeObject(PhotoBig, bw);

        }
    }
}
