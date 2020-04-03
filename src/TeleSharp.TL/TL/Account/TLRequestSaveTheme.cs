using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(-229175188)]
    public class TLRequestSaveTheme : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -229175188;
            }
        }

        public TLAbsInputTheme Theme { get; set; }
        public bool Unsave { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Theme = (TLAbsInputTheme)ObjectUtils.DeserializeObject(br);
            Unsave = BoolUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Theme, bw);
            BoolUtil.Serialize(Unsave, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
