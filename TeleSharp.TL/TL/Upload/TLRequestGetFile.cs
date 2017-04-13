using System.IO;

namespace TeleSharp.TL.Upload
{
    [TLObject(-475607115)]
    public class TLRequestGetFile : TLMethod
    {
        public override int Constructor => -475607115;

        public TLAbsInputFileLocation location { get; set; }
        public int offset { get; set; }
        public int limit { get; set; }
        public TLFile Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            location = (TLAbsInputFileLocation) ObjectUtils.DeserializeObject(br);
            offset = br.ReadInt32();
            limit = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(location, bw);
            bw.Write(offset);
            bw.Write(limit);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLFile) ObjectUtils.DeserializeObject(br);
        }
    }
}