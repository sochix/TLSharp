using System.IO;

namespace TeleSharp.TL.Photos
{
    [TLObject(-1916114267)]
    public class TLPhotos : TLAbsPhotos
    {
        public override int Constructor => -1916114267;

        public TLVector<TLAbsPhoto> photos { get; set; }
        public TLVector<TLAbsUser> users { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            photos = ObjectUtils.DeserializeVector<TLAbsPhoto>(br);
            users = ObjectUtils.DeserializeVector<TLAbsUser>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(photos, bw);
            ObjectUtils.SerializeObject(users, bw);
        }
    }
}