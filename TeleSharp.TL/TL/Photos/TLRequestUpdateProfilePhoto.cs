using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Photos
{
    [TLObject(-256159406)]
    public class TLRequestUpdateProfilePhoto : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -256159406;
            }
        }

        public TLAbsInputPhoto id { get; set; }
        public TLAbsUserProfilePhoto Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = (TLAbsInputPhoto)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(id, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUserProfilePhoto)ObjectUtils.DeserializeObject(br);

        }
    }
}
