using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-534283678)]
    public class TLSecureFile : TLAbsSecureFile
    {
        public override int Constructor
        {
            get
            {
                return -534283678;
            }
        }

        public long Id { get; set; }
        public long AccessHash { get; set; }
        public int Size { get; set; }
        public int DcId { get; set; }
        public int Date { get; set; }
        public byte[] FileHash { get; set; }
        public byte[] Secret { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Id = br.ReadInt64();
            AccessHash = br.ReadInt64();
            Size = br.ReadInt32();
            DcId = br.ReadInt32();
            Date = br.ReadInt32();
            FileHash = BytesUtil.Deserialize(br);
            Secret = BytesUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Id);
            bw.Write(AccessHash);
            bw.Write(Size);
            bw.Write(DcId);
            bw.Write(Date);
            BytesUtil.Serialize(FileHash, bw);
            BytesUtil.Serialize(Secret, bw);

        }
    }
}
