using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-2059962289)]
    public class TLChannelForbidden : TLAbsChat
    {
        public override int Constructor
        {
            get
            {
                return -2059962289;
            }
        }

        public int flags { get; set; }
        public bool broadcast { get; set; }
        public bool megagroup { get; set; }
        public int id { get; set; }
        public long access_hash { get; set; }
        public string title { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = broadcast ? (flags | 32) : (flags & ~32);
            flags = megagroup ? (flags | 256) : (flags & ~256);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            broadcast = (flags & 32) != 0;
            megagroup = (flags & 256) != 0;
            id = br.ReadInt32();
            access_hash = br.ReadInt64();
            title = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);


            bw.Write(id);
            bw.Write(access_hash);
            StringUtil.Serialize(title, bw);

        }
    }
}
