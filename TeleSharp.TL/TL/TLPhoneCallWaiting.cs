using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(462375633)]
    public class TLPhoneCallWaiting : TLAbsPhoneCall
    {
        public override int Constructor
        {
            get
            {
                return 462375633;
            }
        }

        public int flags { get; set; }
        public long id { get; set; }
        public long access_hash { get; set; }
        public int date { get; set; }
        public int admin_id { get; set; }
        public int participant_id { get; set; }
        public TLPhoneCallProtocol protocol { get; set; }
        public int? receive_date { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = receive_date != null ? (flags | 1) : (flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            id = br.ReadInt64();
            access_hash = br.ReadInt64();
            date = br.ReadInt32();
            admin_id = br.ReadInt32();
            participant_id = br.ReadInt32();
            protocol = (TLPhoneCallProtocol)ObjectUtils.DeserializeObject(br);
            if ((flags & 1) != 0)
                receive_date = br.ReadInt32();
            else
                receive_date = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);
            bw.Write(id);
            bw.Write(access_hash);
            bw.Write(date);
            bw.Write(admin_id);
            bw.Write(participant_id);
            ObjectUtils.SerializeObject(protocol, bw);
            if ((flags & 1) != 0)
                bw.Write(receive_date.Value);

        }
    }
}
