using System.IO;
namespace TeleSharp.TL.Messages
{
    [TLObject(1180140658)]
    public class TLRequestGetUnreadMentions : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1180140658;
            }
        }

        public TLAbsInputPeer peer { get; set; }
        public int offset_id { get; set; }
        public int add_offset { get; set; }
        public int limit { get; set; }
        public int max_id { get; set; }
        public int min_id { get; set; }
        public Messages.TLAbsMessages Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            offset_id = br.ReadInt32();
            add_offset = br.ReadInt32();
            limit = br.ReadInt32();
            max_id = br.ReadInt32();
            min_id = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(peer, bw);
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
