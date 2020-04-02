using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(1364105629)]
    public class TLRequestGetInlineBotResults : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1364105629;
            }
        }

        public int Flags { get; set; }
        public TLAbsInputUser Bot { get; set; }
        public TLAbsInputPeer Peer { get; set; }
        public TLAbsInputGeoPoint GeoPoint { get; set; }
        public string Query { get; set; }
        public string Offset { get; set; }
        public Messages.TLBotResults Response { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = GeoPoint != null ? (Flags | 1) : (Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Bot = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
            Peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            if ((Flags & 1) != 0)
                GeoPoint = (TLAbsInputGeoPoint)ObjectUtils.DeserializeObject(br);
            else
                GeoPoint = null;

            Query = StringUtil.Deserialize(br);
            Offset = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);
            ObjectUtils.SerializeObject(Bot, bw);
            ObjectUtils.SerializeObject(Peer, bw);
            if ((Flags & 1) != 0)
                ObjectUtils.SerializeObject(GeoPoint, bw);
            StringUtil.Serialize(Query, bw);
            StringUtil.Serialize(Offset, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLBotResults)ObjectUtils.DeserializeObject(br);

        }
    }
}
