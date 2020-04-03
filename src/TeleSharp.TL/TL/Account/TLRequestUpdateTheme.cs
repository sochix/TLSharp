using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(1555261397)]
    public class TLRequestUpdateTheme : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1555261397;
            }
        }

        public int Flags { get; set; }
        public string Format { get; set; }
        public TLAbsInputTheme Theme { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public TLAbsInputDocument Document { get; set; }
        public TLInputThemeSettings Settings { get; set; }
        public TLTheme Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Format = StringUtil.Deserialize(br);
            Theme = (TLAbsInputTheme)ObjectUtils.DeserializeObject(br);
            if ((Flags & 1) != 0)
                Slug = StringUtil.Deserialize(br);
            else
                Slug = null;

            if ((Flags & 2) != 0)
                Title = StringUtil.Deserialize(br);
            else
                Title = null;

            if ((Flags & 4) != 0)
                Document = (TLAbsInputDocument)ObjectUtils.DeserializeObject(br);
            else
                Document = null;

            if ((Flags & 8) != 0)
                Settings = (TLInputThemeSettings)ObjectUtils.DeserializeObject(br);
            else
                Settings = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            StringUtil.Serialize(Format, bw);
            ObjectUtils.SerializeObject(Theme, bw);
            if ((Flags & 1) != 0)
                StringUtil.Serialize(Slug, bw);
            if ((Flags & 2) != 0)
                StringUtil.Serialize(Title, bw);
            if ((Flags & 4) != 0)
                ObjectUtils.SerializeObject(Document, bw);
            if ((Flags & 8) != 0)
                ObjectUtils.SerializeObject(Settings, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLTheme)ObjectUtils.DeserializeObject(br);

        }
    }
}
