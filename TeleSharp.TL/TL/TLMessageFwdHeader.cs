using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-85986132)]
    public class TLMessageFwdHeader : TLObject
    {
        public int? ChannelId { get; set; }

        public int? ChannelPost { get; set; }

        public override int Constructor
        {
            get
            {
                return -85986132;
            }
        }

        public int Date { get; set; }

        public int Flags { get; set; }

        public int? FromId { get; set; }

        public string PostAuthor { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            if ((Flags & 1) != 0)
                FromId = br.ReadInt32();
            else
                FromId = null;

            Date = br.ReadInt32();
            if ((Flags & 2) != 0)
                ChannelId = br.ReadInt32();
            else
                ChannelId = null;

            if ((Flags & 4) != 0)
                ChannelPost = br.ReadInt32();
            else
                ChannelPost = null;

            if ((Flags & 8) != 0)
                PostAuthor = StringUtil.Deserialize(br);
            else
                PostAuthor = null;
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            if ((Flags & 1) != 0)
                bw.Write(FromId.Value);
            bw.Write(Date);
            if ((Flags & 2) != 0)
                bw.Write(ChannelId.Value);
            if ((Flags & 4) != 0)
                bw.Write(ChannelPost.Value);
            if ((Flags & 8) != 0)
                StringUtil.Serialize(PostAuthor, bw);
        }
    }
}
