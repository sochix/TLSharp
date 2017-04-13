using System.IO;

namespace TeleSharp.TL.Photos
{
    [TLObject(352657236)]
    public class TLPhotosSlice : TLAbsPhotos
    {
        public override int Constructor => 352657236;

        public int count { get; set; }
        public TLVector<TLAbsPhoto> photos { get; set; }
        public TLVector<TLAbsUser> users { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            count = br.ReadInt32();
            photos = ObjectUtils.DeserializeVector<TLAbsPhoto>(br);
            users = ObjectUtils.DeserializeVector<TLAbsUser>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(count);
            ObjectUtils.SerializeObject(photos, bw);
            ObjectUtils.SerializeObject(users, bw);
        }
    }
}