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

        public int Flags { get; set; }
        public int ApiId { get; set; }
        public string ApiHash { get; set; }
        public string BotAuthToken { get; set; }
        public Auth.TLAuthorization Response { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            ApiId = br.ReadInt32();
            ApiHash = StringUtil.Deserialize(br);
            BotAuthToken = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);
            bw.Write(ApiId);
            StringUtil.Serialize(ApiHash, bw);
            StringUtil.Serialize(BotAuthToken, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Auth.TLAuthorization)ObjectUtils.DeserializeObject(br);

        }
    }
}
