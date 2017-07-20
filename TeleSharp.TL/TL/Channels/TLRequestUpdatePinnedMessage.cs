using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Channels
{
    [TLObject(-1490162350)]
    public class TLRequestUpdatePinnedMessage : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1490162350;
            }
        }

        public int flags { get; set; }
        public bool silent { get; set; }
        public TLAbsInputChannel channel { get; set; }
        public int id { get; set; }
        public TLAbsUpdates Response { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = silent ? (flags | 1) : (flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            silent = (flags & 1) != 0;
            channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
            id = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);

            ObjectUtils.SerializeObject(channel, bw);
            bw.Write(id);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

        }
    }
}
