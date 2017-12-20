using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(163765653)]
    public class TLRequestSetBotPrecheckoutResults : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 163765653;
            }
        }

        public string Error { get; set; }

        public int Flags { get; set; }

        public long QueryId { get; set; }

        public bool Response { get; set; }

        public bool Success { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Success = (Flags & 2) != 0;
            QueryId = br.ReadInt64();
            if ((Flags & 1) != 0)
                Error = StringUtil.Deserialize(br);
            else
                Error = null;
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);

            bw.Write(QueryId);
            if ((Flags & 1) != 0)
                StringUtil.Serialize(Error, bw);
        }
    }
}
