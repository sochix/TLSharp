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
        public bool Video { get; set; }
        public long CallId { get; set; }
        public TLAbsPhoneCallDiscardReason Reason { get; set; }
        public int? Duration { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Video = (Flags & 4) != 0;
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
            bw.Write(Flags);

            bw.Write(CallId);
            if ((Flags & 1) != 0)
                ObjectUtils.SerializeObject(Reason, bw);
            if ((Flags & 2) != 0)
                bw.Write(Duration.Value);

        }
    }
}
