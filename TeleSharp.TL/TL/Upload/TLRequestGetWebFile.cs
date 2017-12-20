using System.IO;

namespace TeleSharp.TL.Upload
{
    [TLObject(619086221)]
    public class TLRequestGetWebFile : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 619086221;
            }
        }

        public int Limit { get; set; }

        public TLInputWebFileLocation Location { get; set; }

        public int Offset { get; set; }

        public Upload.TLWebFile Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Location = (TLInputWebFileLocation)ObjectUtils.DeserializeObject(br);
            Offset = br.ReadInt32();
            Limit = br.ReadInt32();
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Upload.TLWebFile)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Location, bw);
            bw.Write(Offset);
            bw.Write(Limit);
        }
    }
}
