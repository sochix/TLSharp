using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Users
{
    [TLObject(-1865902923)]
    public class TLRequestSetSecureValueErrors : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1865902923;
            }
        }

        public TLAbsInputUser Id { get; set; }
        public TLVector<TLAbsSecureValueError> Errors { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Id = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
            Errors = (TLVector<TLAbsSecureValueError>)ObjectUtils.DeserializeVector<TLAbsSecureValueError>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Id, bw);
            ObjectUtils.SerializeObject(Errors, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
