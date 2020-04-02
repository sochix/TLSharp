using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-6249322)]
    public class TLInputStickerSetItem : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -6249322;
            }
        }

        public int Flags { get; set; }
        public TLAbsInputDocument Document { get; set; }
        public string Emoji { get; set; }
        public TLMaskCoords MaskCoords { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = MaskCoords != null ? (Flags | 1) : (Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Document = (TLAbsInputDocument)ObjectUtils.DeserializeObject(br);
            Emoji = StringUtil.Deserialize(br);
            if ((Flags & 1) != 0)
                MaskCoords = (TLMaskCoords)ObjectUtils.DeserializeObject(br);
            else
                MaskCoords = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);
            ObjectUtils.SerializeObject(Document, bw);
            StringUtil.Serialize(Emoji, bw);
            if ((Flags & 1) != 0)
                ObjectUtils.SerializeObject(MaskCoords, bw);

        }
    }
}
