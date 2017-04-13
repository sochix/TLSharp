using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(1013621127)]
    public class TLRequestGetChats : TLMethod
    {
        public override int Constructor => 1013621127;

        public TLVector<int> id { get; set; }
        public TLChats Response { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = ObjectUtils.DeserializeVector<int>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(id, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLChats) ObjectUtils.DeserializeObject(br);
        }
    }
}