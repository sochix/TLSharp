using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(911761060)]
    public class TLBotCallbackAnswer : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 911761060;
            }
        }

        public int flags { get; set; }
        public bool alert { get; set; }
        public bool has_url { get; set; }
        public string message { get; set; }
        public string url { get; set; }
        public int cache_time { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = alert ? (flags | 2) : (flags & ~2);
            flags = has_url ? (flags | 8) : (flags & ~8);
            flags = message != null ? (flags | 1) : (flags & ~1);
            flags = url != null ? (flags | 4) : (flags & ~4);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            alert = (flags & 2) != 0;
            has_url = (flags & 8) != 0;
            if ((flags & 1) != 0)
                message = StringUtil.Deserialize(br);
            else
                message = null;

            if ((flags & 4) != 0)
                url = StringUtil.Deserialize(br);
            else
                url = null;

            cache_time = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);


            if ((flags & 1) != 0)
                StringUtil.Serialize(message, bw);
            if ((flags & 4) != 0)
                StringUtil.Serialize(url, bw);
            bw.Write(cache_time);

        }
    }
}
