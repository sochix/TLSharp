using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Auth
{
    [TLObject(-1313598185)]
    public class TLRequestExportLoginToken : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1313598185;
            }
        }

        public int ApiId { get; set; }
        public string ApiHash { get; set; }
        public TLVector<int> ExceptIds { get; set; }
        public Auth.TLAbsLoginToken Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            ApiId = br.ReadInt32();
            ApiHash = StringUtil.Deserialize(br);
            ExceptIds = (TLVector<int>)ObjectUtils.DeserializeVector<int>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(ApiId);
            StringUtil.Serialize(ApiHash, bw);
            ObjectUtils.SerializeObject(ExceptIds, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Auth.TLAbsLoginToken)ObjectUtils.DeserializeObject(br);

        }
    }
}
