using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(852135825)]
    public class TLRequestGetWebPage : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 852135825;
            }
        }

        public string url { get; set; }
        public int hash { get; set; }
        public TLAbsWebPage Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            url = StringUtil.Deserialize(br);
            hash = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(url, bw);
            bw.Write(hash);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsWebPage)ObjectUtils.DeserializeObject(br);

        }
    }
}
