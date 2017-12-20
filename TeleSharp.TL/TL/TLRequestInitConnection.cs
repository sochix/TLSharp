using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-951575130)]
    public class TLRequestInitConnection : TLMethod
    {
        public int ApiId { get; set; }

        public string AppVersion { get; set; }

        public override int Constructor
        {
            get
            {
                return -951575130;
            }
        }

        public string DeviceModel { get; set; }

        public string LangCode { get; set; }

        public string LangPack { get; set; }

        public TLObject Query { get; set; }

        public TLObject Response { get; set; }

        public string SystemLangCode { get; set; }

        public string SystemVersion { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            ApiId = br.ReadInt32();
            DeviceModel = StringUtil.Deserialize(br);
            SystemVersion = StringUtil.Deserialize(br);
            AppVersion = StringUtil.Deserialize(br);
            SystemLangCode = StringUtil.Deserialize(br);
            LangPack = StringUtil.Deserialize(br);
            LangCode = StringUtil.Deserialize(br);
            Query = (TLObject)ObjectUtils.DeserializeObject(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLObject)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(ApiId);
            StringUtil.Serialize(DeviceModel, bw);
            StringUtil.Serialize(SystemVersion, bw);
            StringUtil.Serialize(AppVersion, bw);
            StringUtil.Serialize(SystemLangCode, bw);
            StringUtil.Serialize(LangPack, bw);
            StringUtil.Serialize(LangCode, bw);
            ObjectUtils.SerializeObject(Query, bw);
        }
    }
}
