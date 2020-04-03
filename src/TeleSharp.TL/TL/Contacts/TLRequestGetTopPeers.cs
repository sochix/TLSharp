using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Contacts
{
    [TLObject(-728224331)]
    public class TLRequestGetTopPeers : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -728224331;
            }
        }

        public int Flags { get; set; }
        public bool Correspondents { get; set; }
        public bool BotsPm { get; set; }
        public bool BotsInline { get; set; }
        public bool PhoneCalls { get; set; }
        public bool ForwardUsers { get; set; }
        public bool ForwardChats { get; set; }
        public bool Groups { get; set; }
        public bool Channels { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
        public int Hash { get; set; }
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
            ForwardUsers = (Flags & 16) != 0;
            ForwardChats = (Flags & 32) != 0;
            Groups = (Flags & 1024) != 0;
            Channels = (Flags & 32768) != 0;
            Offset = br.ReadInt32();
            Limit = br.ReadInt32();
            Hash = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);








            bw.Write(Offset);
            bw.Write(Limit);
            bw.Write(Hash);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Contacts.TLAbsTopPeers)ObjectUtils.DeserializeObject(br);

        }
    }
}
