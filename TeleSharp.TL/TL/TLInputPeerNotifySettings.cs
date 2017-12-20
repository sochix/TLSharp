using System.IO;

namespace TeleSharp.TL
{
    [TLObject(949182130)]
    public class TLInputPeerNotifySettings : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 949182130;
            }
        }

        public int Flags { get; set; }

        public int MuteUntil { get; set; }

        public bool ShowPreviews { get; set; }

        public bool Silent { get; set; }

        public string Sound { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            ShowPreviews = (Flags & 1) != 0;
            Silent = (Flags & 2) != 0;
            MuteUntil = br.ReadInt32();
            Sound = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);


            bw.Write(MuteUntil);
            StringUtil.Serialize(Sound, bw);
        }
    }
}
