using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1556570557)]
    public class TLEmojiKeywordsDifference : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 1556570557;
            }
        }

        public string LangCode { get; set; }
        public int FromVersion { get; set; }
        public int Version { get; set; }
        public TLVector<TLAbsEmojiKeyword> Keywords { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            LangCode = StringUtil.Deserialize(br);
            FromVersion = br.ReadInt32();
            Version = br.ReadInt32();
            Keywords = (TLVector<TLAbsEmojiKeyword>)ObjectUtils.DeserializeVector<TLAbsEmojiKeyword>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(LangCode, bw);
            bw.Write(FromVersion);
            bw.Write(Version);
            ObjectUtils.SerializeObject(Keywords, bw);

        }
    }
}
