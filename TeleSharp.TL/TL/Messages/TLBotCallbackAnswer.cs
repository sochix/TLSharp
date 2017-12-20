using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(911761060)]
    public class TLBotCallbackAnswer : TLObject
    {
        public bool Alert { get; set; }

        public int CacheTime { get; set; }

        public override int Constructor
        {
            get
            {
                return 911761060;
            }
        }

        public int Flags { get; set; }

        public bool HasUrl { get; set; }

        public string Message { get; set; }

        public string Url { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Alert = (Flags & 2) != 0;
            HasUrl = (Flags & 8) != 0;
            if ((Flags & 1) != 0)
                Message = StringUtil.Deserialize(br);
            else
                Message = null;

            if ((Flags & 4) != 0)
                Url = StringUtil.Deserialize(br);
            else
                Url = null;

            CacheTime = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);


            if ((Flags & 1) != 0)
                StringUtil.Serialize(Message, bw);
            if ((Flags & 4) != 0)
                StringUtil.Serialize(Url, bw);
            bw.Write(CacheTime);
        }
    }
}
