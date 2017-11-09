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

        public int Flags { get; set; }
        public long QueryId { get; set; }
        public int UserId { get; set; }
        public string Query { get; set; }
        public TLAbsGeoPoint Geo { get; set; }
        public string Offset { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = Geo != null ? (Flags | 1) : (Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            QueryId = br.ReadInt64();
            UserId = br.ReadInt32();
            Query = StringUtil.Deserialize(br);
            if ((Flags & 1) != 0)
                Geo = (TLAbsGeoPoint)ObjectUtils.DeserializeObject(br);
            else
                Geo = null;

            Offset = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);
            bw.Write(QueryId);
            bw.Write(UserId);
            StringUtil.Serialize(Query, bw);
            if ((Flags & 1) != 0)
                ObjectUtils.SerializeObject(Geo, bw);
            StringUtil.Serialize(Offset, bw);

        }
    }
}
