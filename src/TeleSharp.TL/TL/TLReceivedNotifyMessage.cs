using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1551583367)]
    public class TLReceivedNotifyMessage : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1551583367;
            }
        }

        public int Id { get; set; }
        public int Flags { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Id = br.ReadInt32();
            Flags = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Id);
            bw.Write(Flags);

        }
    }
}
