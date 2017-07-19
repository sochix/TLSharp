using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Auth
{
    [TLObject(1577067778)]
    public class TLSentCode : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 1577067778;
            }
        }

        public int flags { get; set; }
        public bool phone_registered { get; set; }
        public Auth.TLAbsSentCodeType type { get; set; }
        public string phone_code_hash { get; set; }
        public Auth.TLAbsCodeType next_type { get; set; }
        public int? timeout { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = phone_registered ? (flags | 1) : (flags & ~1);
            flags = next_type != null ? (flags | 2) : (flags & ~2);
            flags = timeout != null ? (flags | 4) : (flags & ~4);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            phone_registered = (flags & 1) != 0;
            type = (Auth.TLAbsSentCodeType)ObjectUtils.DeserializeObject(br);
            phone_code_hash = StringUtil.Deserialize(br);
            if ((flags & 2) != 0)
                next_type = (Auth.TLAbsCodeType)ObjectUtils.DeserializeObject(br);
            else
                next_type = null;

            if ((flags & 4) != 0)
                timeout = br.ReadInt32();
            else
                timeout = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);

            ObjectUtils.SerializeObject(type, bw);
            StringUtil.Serialize(phone_code_hash, bw);
            if ((flags & 2) != 0)
                ObjectUtils.SerializeObject(next_type, bw);
            if ((flags & 4) != 0)
                bw.Write(timeout.Value);

        }
    }
}
