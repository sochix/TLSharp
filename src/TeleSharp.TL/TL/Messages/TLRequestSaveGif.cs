using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(846868683)]
    public class TLRequestSaveGif : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 846868683;
            }
        }

        public TLAbsInputDocument Id { get; set; }
        public bool Unsave { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Id = (TLAbsInputDocument)ObjectUtils.DeserializeObject(br);
            Unsave = BoolUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Id, bw);
            BoolUtil.Serialize(Unsave, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
