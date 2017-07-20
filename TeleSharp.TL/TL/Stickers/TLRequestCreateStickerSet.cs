using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Stickers
{
    [TLObject(-1680314774)]
    public class TLRequestCreateStickerSet : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1680314774;
            }
        }

        public int flags { get; set; }
        public bool masks { get; set; }
        public TLAbsInputUser user_id { get; set; }
        public string title { get; set; }
        public string short_name { get; set; }
        public TLVector<TLInputStickerSetItem> stickers { get; set; }
        public Messages.TLStickerSet Response { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = masks ? (flags | 1) : (flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            masks = (flags & 1) != 0;
            user_id = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
            title = StringUtil.Deserialize(br);
            short_name = StringUtil.Deserialize(br);
            stickers = (TLVector<TLInputStickerSetItem>)ObjectUtils.DeserializeVector<TLInputStickerSetItem>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);

            ObjectUtils.SerializeObject(user_id, bw);
            StringUtil.Serialize(title, bw);
            StringUtil.Serialize(short_name, bw);
            ObjectUtils.SerializeObject(stickers, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLStickerSet)ObjectUtils.DeserializeObject(br);

        }
    }
}
