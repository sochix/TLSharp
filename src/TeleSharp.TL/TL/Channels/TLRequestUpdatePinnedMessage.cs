using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Channels
{
    [TLObject(-1490162350)]
    public class TLRequestUpdatePinnedMessage : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1490162350;
            }
        }

        public int Flags { get; set; }
        public bool Silent { get; set; }
        public TLAbsInputChannel Channel { get; set; }
        public int Id { get; set; }
        public TLAbsUpdates Response { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = Silent ? (Flags | 1) : (Flags & ~1);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Silent = (Flags & 1) != 0;
            Channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
            Id = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);

            ObjectUtils.SerializeObject(Channel, bw);
            bw.Write(Id);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

        }
    }
}
