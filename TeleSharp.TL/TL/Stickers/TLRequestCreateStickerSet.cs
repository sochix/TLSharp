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

        public int Flags { get; set; }
        public bool Masks { get; set; }
        public TLAbsInputUser UserId { get; set; }
        public string Title { get; set; }
        public string ShortName { get; set; }
        public TLVector<TLInputStickerSetItem> Stickers { get; set; }
        public Messages.TLStickerSet Response { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = Masks ? (Flags | 1) : (Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Masks = (Flags & 1) != 0;
            UserId = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
            Title = StringUtil.Deserialize(br);
            ShortName = StringUtil.Deserialize(br);
            Stickers = (TLVector<TLInputStickerSetItem>)ObjectUtils.DeserializeVector<TLInputStickerSetItem>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);

            ObjectUtils.SerializeObject(UserId, bw);
            StringUtil.Serialize(Title, bw);
            StringUtil.Serialize(ShortName, bw);
            ObjectUtils.SerializeObject(Stickers, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLStickerSet)ObjectUtils.DeserializeObject(br);

        }
    }
}
