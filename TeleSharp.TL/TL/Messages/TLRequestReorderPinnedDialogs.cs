using System.IO;

namespace TeleSharp.TL.Messages
{
    [TLObject(-1784678844)]
    public class TLRequestReorderPinnedDialogs : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1784678844;
            }
        }

        public int Flags { get; set; }

        public bool Force { get; set; }

        public TLVector<TLAbsInputPeer> Order { get; set; }

        public bool Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Force = (Flags & 1) != 0;
            Order = (TLVector<TLAbsInputPeer>)ObjectUtils.DeserializeVector<TLAbsInputPeer>(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);

            ObjectUtils.SerializeObject(Order, bw);
        }
    }
}
