using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-637606386)]
    public class TLRequestForwardMessages : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -637606386;
            }
        }

        public int Flags { get; set; }
        public bool Silent { get; set; }
        public bool Background { get; set; }
        public bool WithMyScore { get; set; }
        public bool Grouped { get; set; }
        public TLAbsInputPeer FromPeer { get; set; }
        public TLVector<int> Id { get; set; }
        public TLVector<long> RandomId { get; set; }
        public TLAbsInputPeer ToPeer { get; set; }
        public int? ScheduleDate { get; set; }
        public TLAbsUpdates Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Silent = (Flags & 32) != 0;
            Background = (Flags & 64) != 0;
            WithMyScore = (Flags & 256) != 0;
            Grouped = (Flags & 512) != 0;
            FromPeer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            Id = (TLVector<int>)ObjectUtils.DeserializeVector<int>(br);
            RandomId = (TLVector<long>)ObjectUtils.DeserializeVector<long>(br);
            ToPeer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            if ((Flags & 1024) != 0)
                ScheduleDate = br.ReadInt32();
            else
                ScheduleDate = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);




            ObjectUtils.SerializeObject(FromPeer, bw);
            ObjectUtils.SerializeObject(Id, bw);
            ObjectUtils.SerializeObject(RandomId, bw);
            ObjectUtils.SerializeObject(ToPeer, bw);
            if ((Flags & 1024) != 0)
                bw.Write(ScheduleDate.Value);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

        }
    }
}
