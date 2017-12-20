using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-459324)]
    public class TLInputBotInlineResultDocument : TLAbsInputBotInlineResult
    {
        public override int Constructor
        {
            get
            {
                return -459324;
            }
        }

        public string Description { get; set; }

        public TLAbsInputDocument Document { get; set; }

        public int Flags { get; set; }

        public string Id { get; set; }

        public TLAbsInputBotInlineMessage SendMessage { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

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

            Document = (TLAbsInputDocument)ObjectUtils.DeserializeObject(br);
            SendMessage = (TLAbsInputBotInlineMessage)ObjectUtils.DeserializeObject(br);
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
            ObjectUtils.SerializeObject(Document, bw);
            ObjectUtils.SerializeObject(SendMessage, bw);
        }
    }
}
