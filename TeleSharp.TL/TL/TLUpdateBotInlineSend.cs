using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(239663460)]
    public class TLUpdateBotInlineSend : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 239663460;
            }
        }

        public int flags { get; set; }
        public int user_id { get; set; }
        public string query { get; set; }
        public TLAbsGeoPoint geo { get; set; }
        public string id { get; set; }
        public TLInputBotInlineMessageID msg_id { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = geo != null ? (flags | 1) : (flags & ~1);
            flags = msg_id != null ? (flags | 2) : (flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            user_id = br.ReadInt32();
            query = StringUtil.Deserialize(br);
            if ((flags & 1) != 0)
                geo = (TLAbsGeoPoint)ObjectUtils.DeserializeObject(br);
            else
                geo = null;

            id = StringUtil.Deserialize(br);
            if ((flags & 2) != 0)
                msg_id = (TLInputBotInlineMessageID)ObjectUtils.DeserializeObject(br);
            else
                msg_id = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);
            bw.Write(user_id);
            StringUtil.Serialize(query, bw);
            if ((flags & 1) != 0)
                ObjectUtils.SerializeObject(geo, bw);
            StringUtil.Serialize(id, bw);
            if ((flags & 2) != 0)
                ObjectUtils.SerializeObject(msg_id, bw);

        }
    }
}
