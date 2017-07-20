using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(1369162417)]
    public class TLRequestUploadMedia : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1369162417;
            }
        }

        public TLAbsInputPeer peer { get; set; }
        public TLAbsInputMedia media { get; set; }
        public TLAbsMessageMedia Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            media = (TLAbsInputMedia)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(peer, bw);
            ObjectUtils.SerializeObject(media, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsMessageMedia)ObjectUtils.DeserializeObject(br);

        }
    }
}
