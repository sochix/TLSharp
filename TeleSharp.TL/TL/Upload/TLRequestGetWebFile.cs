using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Upload
{
    [TLObject(619086221)]
    public class TLRequestGetWebFile : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 619086221;
            }
        }

        public TLInputWebFileLocation location { get; set; }
        public int offset { get; set; }
        public int limit { get; set; }
        public Upload.TLWebFile Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            location = (TLInputWebFileLocation)ObjectUtils.DeserializeObject(br);
            offset = br.ReadInt32();
            limit = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(location, bw);
            bw.Write(offset);
            bw.Write(limit);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (Upload.TLWebFile)ObjectUtils.DeserializeObject(br);

        }
    }
}
