using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(958863608)]
    public class TLRequestSaveRecentSticker : TLMethod
    {
        public bool Attached { get; set; }

        public override int Constructor
        {
            get
            {
                return 958863608;
            }
        }

        public int Flags { get; set; }

        public TLAbsInputDocument Id { get; set; }

        public bool Response { get; set; }

        public bool Unsave { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Attached = (Flags & 1) != 0;
            Id = (TLAbsInputDocument)ObjectUtils.DeserializeObject(br);
            Unsave = BoolUtil.Deserialize(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);

            ObjectUtils.SerializeObject(Id, bw);
            BoolUtil.Serialize(Unsave, bw);
        }
    }
}
