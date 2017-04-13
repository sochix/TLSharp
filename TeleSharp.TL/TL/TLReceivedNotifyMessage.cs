using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1551583367)]
    public class TLReceivedNotifyMessage : TLObject
    {
        public override int Constructor => -1551583367;

        public int id { get; set; }
        public int flags { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = br.ReadInt32();
            flags = br.ReadInt32();
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);
            bw.Write(id);
        }
    }
}