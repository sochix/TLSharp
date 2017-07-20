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

        public int flags { get; set; }
        public bool correspondents { get; set; }
        public bool bots_pm { get; set; }
        public bool bots_inline { get; set; }
        public bool phone_calls { get; set; }
        public bool groups { get; set; }
        public bool channels { get; set; }
        public int offset { get; set; }
        public int limit { get; set; }
        public int hash { get; set; }
        public Contacts.TLAbsTopPeers Response { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = correspondents ? (flags | 1) : (flags & ~1);
            flags = bots_pm ? (flags | 2) : (flags & ~2);
            flags = bots_inline ? (flags | 4) : (flags & ~4);
            flags = phone_calls ? (flags | 8) : (flags & ~8);
            flags = groups ? (flags | 1024) : (flags & ~1024);
            flags = channels ? (flags | 32768) : (flags & ~32768);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            correspondents = (flags & 1) != 0;
            bots_pm = (flags & 2) != 0;
            bots_inline = (flags & 4) != 0;
            phone_calls = (flags & 8) != 0;
            groups = (flags & 1024) != 0;
            channels = (flags & 32768) != 0;
            offset = br.ReadInt32();
            limit = br.ReadInt32();
            hash = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);






            bw.Write(offset);
            bw.Write(limit);
            bw.Write(hash);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (Contacts.TLAbsTopPeers)ObjectUtils.DeserializeObject(br);

        }
    }
}
