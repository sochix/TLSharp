using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1791935732)]
    public class TLUpdateUserPhoto : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -1791935732;
            }
        }

        public int user_id { get; set; }
        public int date { get; set; }
        public TLAbsUserProfilePhoto photo { get; set; }
        public bool previous { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            user_id = br.ReadInt32();
            date = br.ReadInt32();
            photo = (TLAbsUserProfilePhoto)ObjectUtils.DeserializeObject(br);
            previous = BoolUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(user_id);
            bw.Write(date);
            ObjectUtils.SerializeObject(photo, bw);
            BoolUtil.Serialize(previous, bw);

        }
    }
}
