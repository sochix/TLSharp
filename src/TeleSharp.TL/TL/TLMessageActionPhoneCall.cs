using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-2132731265)]
    public class TLMessageActionPhoneCall : TLAbsMessageAction
    {
        public override int Constructor
        {
            get
            {
                return -2132731265;
            }
        }

        public int Flags { get; set; }
        public long CallId { get; set; }
        public TLAbsPhoneCallDiscardReason Reason { get; set; }
        public int? Duration { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = Reason != null ? (Flags | 1) : (Flags & ~1);
            Flags = Duration != null ? (Flags | 2) : (Flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            CallId = br.ReadInt64();
            if ((Flags & 1) != 0)
                Reason = (TLAbsPhoneCallDiscardReason)ObjectUtils.DeserializeObject(br);
            else
                Reason = null;

            if ((Flags & 2) != 0)
                Duration = br.ReadInt32();
            else
                Duration = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);
            bw.Write(CallId);
            if ((Flags & 1) != 0)
                ObjectUtils.SerializeObject(Reason, bw);
            if ((Flags & 2) != 0)
                bw.Write(Duration.Value);

        }
    }
}
