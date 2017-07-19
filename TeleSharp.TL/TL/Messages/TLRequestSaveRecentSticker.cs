using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(958863608)]
    public class TLRequestSaveRecentSticker : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 958863608;
            }
        }

        public int flags { get; set; }
        public bool attached { get; set; }
        public TLAbsInputDocument id { get; set; }
        public bool unsave { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = attached ? (flags | 1) : (flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            attached = (flags & 1) != 0;
            id = (TLAbsInputDocument)ObjectUtils.DeserializeObject(br);
            unsave = BoolUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);

            ObjectUtils.SerializeObject(id, bw);
            BoolUtil.Serialize(unsave, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
