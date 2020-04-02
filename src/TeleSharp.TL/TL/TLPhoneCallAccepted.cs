using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1828732223)]
    public class TLPhoneCallAccepted : TLAbsPhoneCall
    {
        public override int Constructor
        {
            get
            {
                return 1828732223;
            }
        }

        public long Id { get; set; }
        public long AccessHash { get; set; }
        public int Date { get; set; }
        public int AdminId { get; set; }
        public int ParticipantId { get; set; }
        public byte[] GB { get; set; }
        public TLPhoneCallProtocol Protocol { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Id = br.ReadInt64();
            AccessHash = br.ReadInt64();
            Date = br.ReadInt32();
            AdminId = br.ReadInt32();
            ParticipantId = br.ReadInt32();
            GB = BytesUtil.Deserialize(br);
            Protocol = (TLPhoneCallProtocol)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Id);
            bw.Write(AccessHash);
            bw.Write(Date);
            bw.Write(AdminId);
            bw.Write(ParticipantId);
            BytesUtil.Serialize(GB, bw);
            ObjectUtils.SerializeObject(Protocol, bw);

        }
    }
}
