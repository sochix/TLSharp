using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1679053127)]
    public class TLBotInlineResult : TLAbsBotInlineResult
    {
        public override int Constructor
        {
            get
            {
                return -1679053127;
            }
        }

        public string ContentType { get; set; }

        public string ContentUrl { get; set; }

        public string Description { get; set; }

        public int? Duration { get; set; }

        public int Flags { get; set; }

        public int? H { get; set; }

        public string Id { get; set; }

        public TLAbsBotInlineMessage SendMessage { get; set; }

        public string ThumbUrl { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        public string Url { get; set; }

        public int? W { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Id = StringUtil.Deserialize(br);
            Type = StringUtil.Deserialize(br);
            if ((Flags & 2) != 0)
                Title = StringUtil.Deserialize(br);
            else
                Title = null;

            if ((Flags & 4) != 0)
                Description = StringUtil.Deserialize(br);
            else
                Description = null;

            if ((Flags & 8) != 0)
                Url = StringUtil.Deserialize(br);
            else
                Url = null;

            if ((Flags & 16) != 0)
                ThumbUrl = StringUtil.Deserialize(br);
            else
                ThumbUrl = null;

            if ((Flags & 32) != 0)
                ContentUrl = StringUtil.Deserialize(br);
            else
                ContentUrl = null;

            if ((Flags & 32) != 0)
                ContentType = StringUtil.Deserialize(br);
            else
                ContentType = null;

            if ((Flags & 64) != 0)
                W = br.ReadInt32();
            else
                W = null;

            if ((Flags & 64) != 0)
                H = br.ReadInt32();
            else
                H = null;

            if ((Flags & 128) != 0)
                Duration = br.ReadInt32();
            else
                Duration = null;

            SendMessage = (TLAbsBotInlineMessage)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            StringUtil.Serialize(Id, bw);
            StringUtil.Serialize(Type, bw);
            if ((Flags & 2) != 0)
                StringUtil.Serialize(Title, bw);
            if ((Flags & 4) != 0)
                StringUtil.Serialize(Description, bw);
            if ((Flags & 8) != 0)
                StringUtil.Serialize(Url, bw);
            if ((Flags & 16) != 0)
                StringUtil.Serialize(ThumbUrl, bw);
            if ((Flags & 32) != 0)
                StringUtil.Serialize(ContentUrl, bw);
            if ((Flags & 32) != 0)
                StringUtil.Serialize(ContentType, bw);
            if ((Flags & 64) != 0)
                bw.Write(W.Value);
            if ((Flags & 64) != 0)
                bw.Write(H.Value);
            if ((Flags & 128) != 0)
                bw.Write(Duration.Value);
            ObjectUtils.SerializeObject(SendMessage, bw);
        }
    }
}
