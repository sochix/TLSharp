using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-2025673089)]
    public class TLPhoneCall : TLAbsPhoneCall
    {
        public override int Constructor
        {
            get
            {
                return -2025673089;
            }
        }

        public int Flags { get; set; }
        public bool P2pAllowed { get; set; }
        public long Id { get; set; }
        public long AccessHash { get; set; }
        public int Date { get; set; }
        public int AdminId { get; set; }
        public int ParticipantId { get; set; }
        public byte[] GAOrB { get; set; }
        public long KeyFingerprint { get; set; }
        public TLPhoneCallProtocol Protocol { get; set; }
        public TLVector<TLPhoneConnection> Connections { get; set; }
        public int StartDate { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            P2pAllowed = (Flags & 32) != 0;
            Id = br.ReadInt64();
            AccessHash = br.ReadInt64();
            Date = br.ReadInt32();
            AdminId = br.ReadInt32();
            ParticipantId = br.ReadInt32();
            GAOrB = BytesUtil.Deserialize(br);
            KeyFingerprint = br.ReadInt64();
            Protocol = (TLPhoneCallProtocol)ObjectUtils.DeserializeObject(br);
            Connections = (TLVector<TLPhoneConnection>)ObjectUtils.DeserializeVector<TLPhoneConnection>(br);
            StartDate = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);

            bw.Write(Id);
            bw.Write(AccessHash);
            bw.Write(Date);
            bw.Write(AdminId);
            bw.Write(ParticipantId);
            BytesUtil.Serialize(GAOrB, bw);
            bw.Write(KeyFingerprint);
            ObjectUtils.SerializeObject(Protocol, bw);
            ObjectUtils.SerializeObject(Connections, bw);
            bw.Write(StartDate);

        }
    }
}
