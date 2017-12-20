using System.IO;

namespace TeleSharp.TL.Channels
{
    [TLObject(-1490162350)]
    public class TLRequestUpdatePinnedMessage : TLMethod
    {
        public TLAbsInputChannel Channel { get; set; }

        public override int Constructor
        {
            get
            {
                return -1490162350;
            }
        }

        public int Flags { get; set; }

        public int Id { get; set; }

        public TLAbsUpdates Response { get; set; }

        public bool Silent { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Silent = (Flags & 1) != 0;
            Channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
            Id = br.ReadInt32();
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);

            ObjectUtils.SerializeObject(Channel, bw);
            bw.Write(Id);
        }
    }
}
