using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-317144808)]
    public class TLEncryptedMessage : TLAbsEncryptedMessage
    {
        public override int Constructor
        {
            get
            {
                return -317144808;
            }
        }

        public long RandomId { get; set; }
        public int ChatId { get; set; }
        public int Date { get; set; }
        public byte[] Bytes { get; set; }
        public TLAbsEncryptedFile File { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            RandomId = br.ReadInt64();
            ChatId = br.ReadInt32();
            Date = br.ReadInt32();
            Bytes = BytesUtil.Deserialize(br);
            File = (TLAbsEncryptedFile)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(RandomId);
            bw.Write(ChatId);
            bw.Write(Date);
            BytesUtil.Serialize(Bytes, bw);
            ObjectUtils.SerializeObject(File, bw);

        }
    }
}
