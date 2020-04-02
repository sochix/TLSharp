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

        public int Flags { get; set; }
        public bool Success { get; set; }
        public long QueryId { get; set; }
        public string Error { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = Success ? (Flags | 2) : (Flags & ~2);
            Flags = Error != null ? (Flags | 1) : (Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Success = (Flags & 2) != 0;
            QueryId = br.ReadInt64();
            if ((Flags & 1) != 0)
                Error = StringUtil.Deserialize(br);
            else
                Error = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);

            bw.Write(QueryId);
            if ((Flags & 1) != 0)
                StringUtil.Serialize(Error, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
