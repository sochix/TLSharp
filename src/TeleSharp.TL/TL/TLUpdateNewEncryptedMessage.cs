using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(314359194)]
    public class TLUpdateNewEncryptedMessage : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 314359194;
            }
        }

        public TLAbsEncryptedMessage Message { get; set; }
        public int Qts { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Message = (TLAbsEncryptedMessage)ObjectUtils.DeserializeObject(br);
            Qts = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Message, bw);
            bw.Write(Qts);

        }
    }
}
