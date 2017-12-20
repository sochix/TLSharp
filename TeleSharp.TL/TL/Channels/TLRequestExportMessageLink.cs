using System.IO;

namespace TeleSharp.TL.Channels
{
    [TLObject(-934882771)]
    public class TLRequestExportMessageLink : TLMethod
    {
        public TLAbsInputChannel Channel { get; set; }

        public override int Constructor
        {
            get
            {
                return -934882771;
            }
        }

        public int Id { get; set; }

        public TLExportedMessageLink Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
            Id = br.ReadInt32();
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLExportedMessageLink)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Channel, bw);
            bw.Write(Id);
        }
    }
}
