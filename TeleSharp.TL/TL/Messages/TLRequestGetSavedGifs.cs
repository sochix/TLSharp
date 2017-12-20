using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(-2084618926)]
    public class TLRequestGetSavedGifs : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -2084618926;
            }
        }

        public int Hash { get; set; }

        public Messages.TLAbsSavedGifs Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Hash = br.ReadInt32();
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLAbsSavedGifs)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Hash);
        }
    }
}
