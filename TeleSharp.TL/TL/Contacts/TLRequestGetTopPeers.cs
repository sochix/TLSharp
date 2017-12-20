using System.IO;

namespace TeleSharp.TL.Contacts
{
    [TLObject(-728224331)]
    public class TLRequestGetTopPeers : TLMethod
    {
        public bool BotsInline { get; set; }

        public bool BotsPm { get; set; }

        public bool Channels { get; set; }

        public override int Constructor
        {
            get
            {
                return -728224331;
            }
        }

        public bool Correspondents { get; set; }

        public int Flags { get; set; }

        public bool Groups { get; set; }

        public int Hash { get; set; }

        public int Limit { get; set; }

        public int Offset { get; set; }

        public bool PhoneCalls { get; set; }

        public Contacts.TLAbsTopPeers Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Correspondents = (Flags & 1) != 0;
            BotsPm = (Flags & 2) != 0;
            BotsInline = (Flags & 4) != 0;
            PhoneCalls = (Flags & 8) != 0;
            Groups = (Flags & 1024) != 0;
            Channels = (Flags & 32768) != 0;
            Offset = br.ReadInt32();
            Limit = br.ReadInt32();
            Hash = br.ReadInt32();
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Contacts.TLAbsTopPeers)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);






            bw.Write(Offset);
            bw.Write(Limit);
            bw.Write(Hash);
        }
    }
}
