using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Help
{
    [TLObject(497489295)]
    public class TLAppUpdate : TLAbsAppUpdate
    {
        public override int Constructor
        {
            get
            {
                return 497489295;
            }
        }

        public int Flags { get; set; }
        public bool CanNotSkip { get; set; }
        public int Id { get; set; }
        public string Version { get; set; }
        public string Text { get; set; }
        public TLVector<TLAbsMessageEntity> Entities { get; set; }
        public TLAbsDocument Document { get; set; }
        public string Url { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            CanNotSkip = (Flags & 1) != 0;
            Id = br.ReadInt32();
            Version = StringUtil.Deserialize(br);
            Text = StringUtil.Deserialize(br);
            Entities = (TLVector<TLAbsMessageEntity>)ObjectUtils.DeserializeVector<TLAbsMessageEntity>(br);
            if ((Flags & 2) != 0)
                Document = (TLAbsDocument)ObjectUtils.DeserializeObject(br);
            else
                Document = null;

            if ((Flags & 4) != 0)
                Url = StringUtil.Deserialize(br);
            else
                Url = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);

            bw.Write(Id);
            StringUtil.Serialize(Version, bw);
            StringUtil.Serialize(Text, bw);
            ObjectUtils.SerializeObject(Entities, bw);
            if ((Flags & 2) != 0)
                ObjectUtils.SerializeObject(Document, bw);
            if ((Flags & 4) != 0)
                StringUtil.Serialize(Url, bw);

        }
    }
}
