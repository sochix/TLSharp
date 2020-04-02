using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1995686519)]
    public class TLInputBotInlineMessageID : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1995686519;
            }
        }

        public int DcId { get; set; }
        public long Id { get; set; }
        public long AccessHash { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            DcId = br.ReadInt32();
            Id = br.ReadInt64();
            AccessHash = br.ReadInt64();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(DcId);
            bw.Write(Id);
            bw.Write(AccessHash);

        }
    }
}
