using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1656358105)]
    public class TLUpdateNewChannelMessage : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return 1656358105;
            }
        }

        public TLAbsMessage Message { get; set; }
        public int Pts { get; set; }
        public int PtsCount { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Message = (TLAbsMessage)ObjectUtils.DeserializeObject(br);
            Pts = br.ReadInt32();
            PtsCount = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Message, bw);
            bw.Write(Pts);
            bw.Write(PtsCount);

        }
    }
}
