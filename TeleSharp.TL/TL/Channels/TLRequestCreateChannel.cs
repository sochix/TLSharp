using System.IO;

namespace TeleSharp.TL.Channels
{
    [TLObject(-192332417)]
    public class TLRequestCreateChannel : TLMethod
    {
        public string About { get; set; }

        public bool Broadcast { get; set; }

        public override int Constructor
        {
            get
            {
                return -192332417;
            }
        }

        public int Flags { get; set; }

        public bool Megagroup { get; set; }

        public TLAbsUpdates Response { get; set; }

        public string Title { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Broadcast = (Flags & 1) != 0;
            Megagroup = (Flags & 2) != 0;
            Title = StringUtil.Deserialize(br);
            About = StringUtil.Deserialize(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);


            StringUtil.Serialize(Title, bw);
            StringUtil.Serialize(About, bw);
        }
    }
}
