using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(163765653)]
    public class TLRequestSetBotPrecheckoutResults : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 163765653;
            }
        }

        public int flags { get; set; }
        public bool success { get; set; }
        public long query_id { get; set; }
        public string error { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = success ? (flags | 2) : (flags & ~2);
            flags = error != null ? (flags | 1) : (flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            success = (flags & 2) != 0;
            query_id = br.ReadInt64();
            if ((flags & 1) != 0)
                error = StringUtil.Deserialize(br);
            else
                error = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);

            bw.Write(query_id);
            if ((flags & 1) != 0)
                StringUtil.Serialize(error, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
