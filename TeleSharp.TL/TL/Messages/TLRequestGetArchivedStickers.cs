using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(1475442322)]
    public class TLRequestGetArchivedStickers : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1475442322;
            }
        }

        public int flags { get; set; }
        public bool masks { get; set; }
        public long offset_id { get; set; }
        public int limit { get; set; }
        public Messages.TLArchivedStickers Response { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = masks ? (flags | 1) : (flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            masks = (flags & 1) != 0;
            offset_id = br.ReadInt64();
            limit = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);

            bw.Write(offset_id);
            bw.Write(limit);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLArchivedStickers)ObjectUtils.DeserializeObject(br);

        }
    }
}
