using System.IO;

namespace TeleSharp.TL.Auth
{
    [TLObject(1738800940)]
    public class TLRequestImportBotAuthorization : TLMethod
    {
        public string ApiHash { get; set; }

        public int ApiId { get; set; }

        public string BotAuthToken { get; set; }

        public override int Constructor
        {
            get
            {
                return 1738800940;
            }
        }

        public int Flags { get; set; }

        public Auth.TLAuthorization Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            ApiId = br.ReadInt32();
            ApiHash = StringUtil.Deserialize(br);
            BotAuthToken = StringUtil.Deserialize(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Auth.TLAuthorization)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            bw.Write(ApiId);
            StringUtil.Serialize(ApiHash, bw);
            StringUtil.Serialize(BotAuthToken, bw);
        }
    }
}
