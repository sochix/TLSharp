using System.IO;
namespace TeleSharp.TL
{
    [TLObject(1489977929)]
    public class TLChannelBannedRights : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 1489977929;
            }
        }

        public int flags { get; set; }
        public bool view_messages { get; set; }
        public bool send_messages { get; set; }
        public bool send_media { get; set; }
        public bool send_stickers { get; set; }
        public bool send_gifs { get; set; }
        public bool send_games { get; set; }
        public bool send_inline { get; set; }
        public bool embed_links { get; set; }
        public int until_date { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = view_messages ? (flags | 1) : (flags & ~1);
            flags = send_messages ? (flags | 2) : (flags & ~2);
            flags = send_media ? (flags | 4) : (flags & ~4);
            flags = send_stickers ? (flags | 8) : (flags & ~8);
            flags = send_gifs ? (flags | 16) : (flags & ~16);
            flags = send_games ? (flags | 32) : (flags & ~32);
            flags = send_inline ? (flags | 64) : (flags & ~64);
            flags = embed_links ? (flags | 128) : (flags & ~128);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            view_messages = (flags & 1) != 0;
            send_messages = (flags & 2) != 0;
            send_media = (flags & 4) != 0;
            send_stickers = (flags & 8) != 0;
            send_gifs = (flags & 16) != 0;
            send_games = (flags & 32) != 0;
            send_inline = (flags & 64) != 0;
            embed_links = (flags & 128) != 0;
            until_date = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);








            bw.Write(until_date);

        }
    }
}
