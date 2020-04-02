using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-732523960)]
    public class TLRequestSearch : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -732523960;
            }
        }

        public int Flags { get; set; }
        public TLAbsInputPeer Peer { get; set; }
        public string Q { get; set; }
        public TLAbsMessagesFilter Filter { get; set; }
        public int MinDate { get; set; }
        public int MaxDate { get; set; }
        public int Offset { get; set; }
        public int MaxId { get; set; }
        public int Limit { get; set; }
        public Messages.TLAbsMessages Response { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            Q = StringUtil.Deserialize(br);
            Filter = (TLAbsMessagesFilter)ObjectUtils.DeserializeObject(br);
            MinDate = br.ReadInt32();
            MaxDate = br.ReadInt32();
            Offset = br.ReadInt32();
            MaxId = br.ReadInt32();
            Limit = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);
            ObjectUtils.SerializeObject(Peer, bw);
            StringUtil.Serialize(Q, bw);
            ObjectUtils.SerializeObject(Filter, bw);
            bw.Write(MinDate);
            bw.Write(MaxDate);
            bw.Write(Offset);
            bw.Write(MaxId);
            bw.Write(Limit);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLAbsMessages)ObjectUtils.DeserializeObject(br);

        }
    }
}
