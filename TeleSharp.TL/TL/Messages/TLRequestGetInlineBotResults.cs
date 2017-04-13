using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(1364105629)]
    public class TLRequestGetInlineBotResults : TLMethod
    {
        public override int Constructor => 1364105629;

        public int flags { get; set; }
        public TLAbsInputUser bot { get; set; }
        public TLAbsInputPeer peer { get; set; }
        public TLAbsInputGeoPoint geo_point { get; set; }
        public string query { get; set; }
        public string offset { get; set; }
        public TLBotResults Response { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = geo_point != null ? flags | 1 : flags & ~1;
        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            bot = (TLAbsInputUser) ObjectUtils.DeserializeObject(br);
            peer = (TLAbsInputPeer) ObjectUtils.DeserializeObject(br);
            if ((flags & 1) != 0)
                geo_point = (TLAbsInputGeoPoint) ObjectUtils.DeserializeObject(br);
            else
                geo_point = null;

            query = StringUtil.Deserialize(br);
            offset = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);
            ObjectUtils.SerializeObject(bot, bw);
            ObjectUtils.SerializeObject(peer, bw);
            if ((flags & 1) != 0)
                ObjectUtils.SerializeObject(geo_point, bw);
            StringUtil.Serialize(query, bw);
            StringUtil.Serialize(offset, bw);
        }

        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLBotResults) ObjectUtils.DeserializeObject(br);
        }
    }
}