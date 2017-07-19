using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Bots
{
    [TLObject(-1440257555)]
    public class TLRequestSendCustomRequest : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1440257555;
            }
        }

        public string custom_method { get; set; }
        public TLDataJSON @params { get; set; }
        public TLDataJSON Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            custom_method = StringUtil.Deserialize(br);
            @params = (TLDataJSON)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(custom_method, bw);
            ObjectUtils.SerializeObject(@params, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLDataJSON)ObjectUtils.DeserializeObject(br);

        }
    }
}
