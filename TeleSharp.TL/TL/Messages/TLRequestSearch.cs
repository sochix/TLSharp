using System.IO;
namespace TeleSharp.TL.Messages
{
    [TLObject(60726944)]
    public class TLRequestSearch : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 60726944;
            }
        }

        public int flags { get; set; }
        public TLAbsInputPeer peer { get; set; }
        public string q { get; set; }
        public TLAbsInputUser from_id { get; set; }
        public TLAbsMessagesFilter filter { get; set; }
        public int min_date { get; set; }
        public int max_date { get; set; }
        public int offset_id { get; set; }
        public int add_offset { get; set; }
        public int limit { get; set; }
        public int max_id { get; set; }
        public int min_id { get; set; }
        public Messages.TLAbsMessages Response { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = from_id != null ? (flags | 1) : (flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            q = StringUtil.Deserialize(br);
            if ((flags & 1) != 0)
                from_id = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
            else
                from_id = null;

            filter = (TLAbsMessagesFilter)ObjectUtils.DeserializeObject(br);
            min_date = br.ReadInt32();
            max_date = br.ReadInt32();
            offset_id = br.ReadInt32();
            add_offset = br.ReadInt32();
            limit = br.ReadInt32();
            max_id = br.ReadInt32();
            min_id = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);
            ObjectUtils.SerializeObject(peer, bw);
            StringUtil.Serialize(q, bw);
            if ((flags & 1) != 0)
                ObjectUtils.SerializeObject(from_id, bw);
            ObjectUtils.SerializeObject(filter, bw);
            bw.Write(min_date);
            bw.Write(max_date);
            bw.Write(offset_id);
            bw.Write(add_offset);
            bw.Write(limit);
            bw.Write(max_id);
            bw.Write(min_id);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLAbsMessages)ObjectUtils.DeserializeObject(br);

        }
    }
}
