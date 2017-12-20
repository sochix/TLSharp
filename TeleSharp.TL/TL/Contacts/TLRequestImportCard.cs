using System.IO;

namespace TeleSharp.TL.Contacts
{
    [TLObject(1340184318)]
    public class TLRequestImportCard : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1340184318;
            }
        }

        public TLVector<int> ExportCard { get; set; }

        public TLAbsUser Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            ExportCard = (TLVector<int>)ObjectUtils.DeserializeVector<int>(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUser)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(ExportCard, bw);
        }
    }
}
