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

        public TLAbsInputPhoto Id { get; set; }
        public TLAbsUserProfilePhoto Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Id = (TLAbsInputPhoto)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Id, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUserProfilePhoto)ObjectUtils.DeserializeObject(br);

        }
    }
}
