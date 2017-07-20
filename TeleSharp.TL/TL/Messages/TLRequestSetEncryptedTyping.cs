using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(2031374829)]
    public class TLRequestSetEncryptedTyping : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 2031374829;
            }
        }

        public TLInputEncryptedChat peer { get; set; }
        public bool typing { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            peer = (TLInputEncryptedChat)ObjectUtils.DeserializeObject(br);
            typing = BoolUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(peer, bw);
            BoolUtil.Serialize(typing, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
