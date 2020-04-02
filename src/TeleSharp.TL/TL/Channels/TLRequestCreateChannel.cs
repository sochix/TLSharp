using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Channels
{
    [TLObject(-192332417)]
    public class TLRequestCreateChannel : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -192332417;
            }
        }

        public int Flags { get; set; }
        public bool Broadcast { get; set; }
        public bool Megagroup { get; set; }
        public string Title { get; set; }
        public string About { get; set; }
        public TLAbsUpdates Response { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = Broadcast ? (Flags | 1) : (Flags & ~1);
            Flags = Megagroup ? (Flags | 2) : (Flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Broadcast = (Flags & 1) != 0;
            Megagroup = (Flags & 2) != 0;
            Title = StringUtil.Deserialize(br);
            About = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);


            StringUtil.Serialize(Title, bw);
            StringUtil.Serialize(About, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

        }
    }
}
