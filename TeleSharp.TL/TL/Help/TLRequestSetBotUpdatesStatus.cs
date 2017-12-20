using System.IO;

namespace TeleSharp.TL.Help
{
    [TLObject(-333262899)]
    public class TLRequestSetBotUpdatesStatus : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -333262899;
            }
        }

        public string Message { get; set; }

        public int PendingUpdatesCount { get; set; }

        public bool Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            PendingUpdatesCount = br.ReadInt32();
            Message = StringUtil.Deserialize(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(PendingUpdatesCount);
            StringUtil.Serialize(Message, bw);
        }
    }
}
