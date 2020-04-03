using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(681420594)]
    public class TLChannelForbidden : TLAbsChat
    {
        public override int Constructor
        {
            get
            {
                return 681420594;
            }
        }

        public int Flags { get; set; }
        public bool Broadcast { get; set; }
        public bool Megagroup { get; set; }
        public int Id { get; set; }
        public long AccessHash { get; set; }
        public string Title { get; set; }
        public int? UntilDate { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Broadcast = (Flags & 32) != 0;
            Megagroup = (Flags & 256) != 0;
            Id = br.ReadInt32();
            AccessHash = br.ReadInt64();
            Title = StringUtil.Deserialize(br);
            if ((Flags & 65536) != 0)
                UntilDate = br.ReadInt32();
            else
                UntilDate = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);


            bw.Write(Id);
            bw.Write(AccessHash);
            StringUtil.Serialize(Title, bw);
            if ((Flags & 65536) != 0)
                bw.Write(UntilDate.Value);

        }
    }
}
