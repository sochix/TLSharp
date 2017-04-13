using System.IO;

namespace TeleSharp.TL.Photos
{
    [TLObject(539045032)]
    public class TLPhoto : TLObject
    {
        public override int Constructor => 539045032;

        public TLAbsPhoto photo { get; set; }
        public TLVector<TLAbsUser> users { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            photo = (TLAbsPhoto) ObjectUtils.DeserializeObject(br);
            users = ObjectUtils.DeserializeVector<TLAbsUser>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(photo, bw);
            ObjectUtils.SerializeObject(users, bw);
        }
    }
}