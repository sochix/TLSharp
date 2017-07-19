using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1264392051)]
    public class TLUpdateEncryption : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -1264392051;
            }
        }

        public TLAbsEncryptedChat chat { get; set; }
        public int date { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            chat = (TLAbsEncryptedChat)ObjectUtils.DeserializeObject(br);
            date = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(chat, bw);
            bw.Write(date);

        }
    }
}
