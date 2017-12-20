using System.IO;

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

        public int Flags { get; set; }

        public TLAbsGeoPoint Geo { get; set; }

        public string Id { get; set; }

        public TLInputBotInlineMessageID MsgId { get; set; }

        public string Query { get; set; }

        public int UserId { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            UserId = br.ReadInt32();
            Query = StringUtil.Deserialize(br);
            if ((Flags & 1) != 0)
                Geo = (TLAbsGeoPoint)ObjectUtils.DeserializeObject(br);
            else
                Geo = null;

            Id = StringUtil.Deserialize(br);
            if ((Flags & 2) != 0)
                MsgId = (TLInputBotInlineMessageID)ObjectUtils.DeserializeObject(br);
            else
                MsgId = null;
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            bw.Write(UserId);
            StringUtil.Serialize(Query, bw);
            if ((Flags & 1) != 0)
                ObjectUtils.SerializeObject(Geo, bw);
            StringUtil.Serialize(Id, bw);
            if ((Flags & 2) != 0)
                ObjectUtils.SerializeObject(MsgId, bw);
        }
    }
}
