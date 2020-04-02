using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-443640366)]
    public class TLRequestDeleteMessages : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -443640366;
            }
        }

        public int Flags { get; set; }
        public bool Revoke { get; set; }
        public TLVector<int> Id { get; set; }
        public Messages.TLAffectedMessages Response { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = Revoke ? (Flags | 1) : (Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Revoke = (Flags & 1) != 0;
            Id = (TLVector<int>)ObjectUtils.DeserializeVector<int>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);

            ObjectUtils.SerializeObject(Id, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLAffectedMessages)ObjectUtils.DeserializeObject(br);

        }
    }
}
