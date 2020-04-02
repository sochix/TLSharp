using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Payments
{
    [TLObject(-667062079)]
    public class TLRequestClearSavedInfo : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -667062079;
            }
        }

        public int Flags { get; set; }
        public bool Credentials { get; set; }
        public bool Info { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = Credentials ? (Flags | 1) : (Flags & ~1);
            Flags = Info ? (Flags | 2) : (Flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Credentials = (Flags & 1) != 0;
            Info = (Flags & 2) != 0;

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);



        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
