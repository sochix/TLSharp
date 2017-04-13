using System.IO;

namespace TeleSharp.TL.Auth
{
    [TLObject(-440401971)]
    public class TLRequestExportAuthorization : TLMethod
    {
        public override int Constructor => -440401971;

        public int dc_id { get; set; }
        public TLExportedAuthorization Response { get; set; }


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
            Response = (TLExportedAuthorization) ObjectUtils.DeserializeObject(br);
        }
    }
}