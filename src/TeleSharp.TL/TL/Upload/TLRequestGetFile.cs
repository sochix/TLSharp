using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Upload
{
    [TLObject(-475607115)]
    public class TLRequestGetFile : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -475607115;
            }
        }

        public TLAbsInputFileLocation Location { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
        public Upload.TLAbsFile Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Location = (TLAbsInputFileLocation)ObjectUtils.DeserializeObject(br);
            Offset = br.ReadInt32();
            Limit = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Location, bw);
            bw.Write(Offset);
            bw.Write(Limit);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Upload.TLAbsFile)ObjectUtils.DeserializeObject(br);

        }
    }
}
