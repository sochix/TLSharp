using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(-923703407)]
    public class TLRequestSendMedia : TLMethod
    {
        public bool Background { get; set; }

        public bool ClearDraft { get; set; }

        public override int Constructor
        {
            get
            {
                return -923703407;
            }
        }

        public int Flags { get; set; }

        public TLAbsInputMedia Media { get; set; }

        public TLAbsInputPeer Peer { get; set; }

        public long RandomId { get; set; }

        public TLAbsReplyMarkup ReplyMarkup { get; set; }

        public int? ReplyToMsgId { get; set; }

        public TLAbsUpdates Response { get; set; }

        public bool Silent { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Silent = (Flags & 32) != 0;
            Background = (Flags & 64) != 0;
            ClearDraft = (Flags & 128) != 0;
            Peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            if ((Flags & 1) != 0)
                ReplyToMsgId = br.ReadInt32();
            else
                ReplyToMsgId = null;

            Media = (TLAbsInputMedia)ObjectUtils.DeserializeObject(br);
            RandomId = br.ReadInt64();
            if ((Flags & 4) != 0)
                ReplyMarkup = (TLAbsReplyMarkup)ObjectUtils.DeserializeObject(br);
            else
                ReplyMarkup = null;
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);



            ObjectUtils.SerializeObject(Peer, bw);
            if ((Flags & 1) != 0)
                bw.Write(ReplyToMsgId.Value);
            ObjectUtils.SerializeObject(Media, bw);
            bw.Write(RandomId);
            if ((Flags & 4) != 0)
                ObjectUtils.SerializeObject(ReplyMarkup, bw);
        }
    }
}
