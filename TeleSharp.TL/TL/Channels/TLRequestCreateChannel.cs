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

        public int flags { get; set; }
        public bool broadcast { get; set; }
        public bool megagroup { get; set; }
        public string title { get; set; }
        public string about { get; set; }
        public TLAbsUpdates Response { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = broadcast ? (flags | 1) : (flags & ~1);
            flags = megagroup ? (flags | 2) : (flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            broadcast = (flags & 1) != 0;
            megagroup = (flags & 2) != 0;
            title = StringUtil.Deserialize(br);
            about = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);


            StringUtil.Serialize(title, bw);
            StringUtil.Serialize(about, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

        }
    }
}
