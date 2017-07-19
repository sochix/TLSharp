using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1769565673)]
    public class TLRequestInitConnection : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1769565673;
            }
        }

        public int api_id { get; set; }
        public string device_model { get; set; }
        public string system_version { get; set; }
        public string app_version { get; set; }
        public string lang_code { get; set; }
        public TLObject query { get; set; }
        public TLObject Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            api_id = br.ReadInt32();
            device_model = StringUtil.Deserialize(br);
            system_version = StringUtil.Deserialize(br);
            app_version = StringUtil.Deserialize(br);
            lang_code = StringUtil.Deserialize(br);
            query = (TLObject)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(api_id);
            StringUtil.Serialize(device_model, bw);
            StringUtil.Serialize(system_version, bw);
            StringUtil.Serialize(app_version, bw);
            StringUtil.Serialize(lang_code, bw);
            ObjectUtils.SerializeObject(query, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLObject)ObjectUtils.DeserializeObject(br);

        }
    }
}
