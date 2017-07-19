using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Auth
{
    [TLObject(1738800940)]
    public class TLRequestImportBotAuthorization : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1738800940;
            }
        }

        public int flags { get; set; }
        public int api_id { get; set; }
        public string api_hash { get; set; }
        public string bot_auth_token { get; set; }
        public Auth.TLAuthorization Response { get; set; }


        public void ComputeFlags()
        {
            flags = 0;

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            api_id = br.ReadInt32();
            api_hash = StringUtil.Deserialize(br);
            bot_auth_token = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);
            bw.Write(api_id);
            StringUtil.Serialize(api_hash, bw);
            StringUtil.Serialize(bot_auth_token, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (Auth.TLAuthorization)ObjectUtils.DeserializeObject(br);

        }
    }
}
