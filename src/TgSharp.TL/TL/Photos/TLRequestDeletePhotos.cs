using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL.Photos
{
    [TLObject(-2016444625)]
    public class TLRequestDeletePhotos : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -2016444625;
            }
        }

        public TLVector<TLAbsInputPhoto> Id { get; set; }
        public TLVector<long> Response { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Id = (TLVector<TLAbsInputPhoto>)ObjectUtils.DeserializeVector<TLAbsInputPhoto>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Id, bw);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLVector<long>)ObjectUtils.DeserializeVector<long>(br);
        }
    }
}
