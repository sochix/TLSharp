using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Auth
{
    [TLObject(-440401971)]
    public class TLRequestExportAuthorization : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -440401971;
            }
        }

        public int dc_id { get; set; }
        public Auth.TLExportedAuthorization Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            dc_id = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(dc_id);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (Auth.TLExportedAuthorization)ObjectUtils.DeserializeObject(br);

        }
    }
}
