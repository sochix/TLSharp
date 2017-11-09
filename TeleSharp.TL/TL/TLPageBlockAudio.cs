using System.IO;
namespace TeleSharp.TL
{
    [TLObject(834148991)]
    public class TLPageBlockAudio : TLAbsPageBlock
    {
        public override int Constructor
        {
            get
            {
                return 834148991;
            }
        }

        public long audio_id { get; set; }
        public TLAbsRichText caption { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            audio_id = br.ReadInt64();
            caption = (TLAbsRichText)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(audio_id);
            ObjectUtils.SerializeObject(caption, bw);

        }
    }
}
