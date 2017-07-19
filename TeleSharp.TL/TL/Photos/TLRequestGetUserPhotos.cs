using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Photos
{
    [TLObject(-1848823128)]
    public class TLRequestGetUserPhotos : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1848823128;
            }
        }

        public TLAbsInputUser user_id { get; set; }
        public int offset { get; set; }
        public long max_id { get; set; }
        public int limit { get; set; }
        public Photos.TLAbsPhotos Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            user_id = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
            offset = br.ReadInt32();
            max_id = br.ReadInt64();
            limit = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(user_id, bw);
            bw.Write(offset);
            bw.Write(max_id);
            bw.Write(limit);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (Photos.TLAbsPhotos)ObjectUtils.DeserializeObject(br);

        }
    }
}
