using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-247351839)]
    public class TLInputEncryptedChat : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -247351839;
            }
        }

        public int chat_id { get; set; }
        public long access_hash { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            chat_id = br.ReadInt32();
            access_hash = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(chat_id);
            bw.Write(access_hash);

        }
    }
}
