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

        public long photo_id { get; set; }
        public TLAbsFileLocation photo_small { get; set; }
        public TLAbsFileLocation photo_big { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            photo_id = br.ReadInt64();
            photo_small = (TLAbsFileLocation)ObjectUtils.DeserializeObject(br);
            photo_big = (TLAbsFileLocation)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(photo_id);
            ObjectUtils.SerializeObject(photo_small, bw);
            ObjectUtils.SerializeObject(photo_big, bw);

        }
    }
}
