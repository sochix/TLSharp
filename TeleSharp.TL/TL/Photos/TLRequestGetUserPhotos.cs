using System.IO;

namespace TeleSharp.TL.Photos
{
    [TLObject(-1848823128)]
    public class TLRequestGetUserPhotos : TLMethod
    {
        public override int Constructor => -1848823128;

        public TLAbsInputUser user_id { get; set; }
        public int offset { get; set; }
        public long max_id { get; set; }
        public int limit { get; set; }
        public TLAbsPhotos Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            user_id = (TLAbsInputUser) ObjectUtils.DeserializeObject(br);
            offset = br.ReadInt32();
            max_id = br.ReadInt64();
            limit = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(user_id, bw);
            bw.Write(offset);
            bw.Write(max_id);
            bw.Write(limit);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsPhotos) ObjectUtils.DeserializeObject(br);
        }
    }
}