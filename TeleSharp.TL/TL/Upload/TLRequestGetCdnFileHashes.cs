using System.IO;

namespace TeleSharp.TL.Upload
{
    [TLObject(-149567365)]
    public class TLRequestGetCdnFileHashes : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -149567365;
            }
        }

        public byte[] FileToken { get; set; }

        public int Offset { get; set; }

        public TLVector<TLCdnFileHash> Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            FileToken = BytesUtil.Deserialize(br);
            Offset = br.ReadInt32();
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLVector<TLCdnFileHash>)ObjectUtils.DeserializeVector<TLCdnFileHash>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            BytesUtil.Serialize(FileToken, bw);
            bw.Write(Offset);
        }
    }
}
