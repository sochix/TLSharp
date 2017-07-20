using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1417832080)]
    public class TLUpdateBotInlineQuery : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 1417832080;
            }
        }

        public int flags { get; set; }
        public long query_id { get; set; }
        public int user_id { get; set; }
        public string query { get; set; }
        public TLAbsGeoPoint geo { get; set; }
        public string offset { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = geo != null ? (flags | 1) : (flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            query_id = br.ReadInt64();
            user_id = br.ReadInt32();
            query = StringUtil.Deserialize(br);
            if ((flags & 1) != 0)
                geo = (TLAbsGeoPoint)ObjectUtils.DeserializeObject(br);
            else
                geo = null;

            offset = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);
            bw.Write(query_id);
            bw.Write(user_id);
            StringUtil.Serialize(query, bw);
            if ((flags & 1) != 0)
                ObjectUtils.SerializeObject(geo, bw);
            StringUtil.Serialize(offset, bw);

        }
    }
}
