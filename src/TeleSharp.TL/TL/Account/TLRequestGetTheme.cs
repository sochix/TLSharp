using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(-1919060949)]
    public class TLRequestGetTheme : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1919060949;
            }
        }

        public string Format { get; set; }
        public TLAbsInputTheme Theme { get; set; }
        public long DocumentId { get; set; }
        public TLTheme Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Format = StringUtil.Deserialize(br);
            Theme = (TLAbsInputTheme)ObjectUtils.DeserializeObject(br);
            DocumentId = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Format, bw);
            ObjectUtils.SerializeObject(Theme, bw);
            bw.Write(DocumentId);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLTheme)ObjectUtils.DeserializeObject(br);

        }
    }
}
