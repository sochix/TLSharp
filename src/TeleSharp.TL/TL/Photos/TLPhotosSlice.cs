using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Photos
{
    [TLObject(352657236)]
    public class TLPhotosSlice : TLAbsPhotos
    {
        public override int Constructor
        {
            get
            {
                return 352657236;
            }
        }

        public int Count { get; set; }
        public TLVector<TLAbsPhoto> Photos { get; set; }
        public TLVector<TLAbsUser> Users { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Count = br.ReadInt32();
            Photos = (TLVector<TLAbsPhoto>)ObjectUtils.DeserializeVector<TLAbsPhoto>(br);
            Users = (TLVector<TLAbsUser>)ObjectUtils.DeserializeVector<TLAbsUser>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Count);
            ObjectUtils.SerializeObject(Photos, bw);
            ObjectUtils.SerializeObject(Users, bw);

        }
    }
}
