using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1657903163)]
    public class TLUpdateContactLink : TLAbsUpdate
    {
        public override int Constructor
        {
            get
            {
                return -1657903163;
            }
        }

        public int user_id { get; set; }
        public TLAbsContactLink my_link { get; set; }
        public TLAbsContactLink foreign_link { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            user_id = br.ReadInt32();
            my_link = (TLAbsContactLink)ObjectUtils.DeserializeObject(br);
            foreign_link = (TLAbsContactLink)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(user_id);
            ObjectUtils.SerializeObject(my_link, bw);
            ObjectUtils.SerializeObject(foreign_link, bw);

        }
    }
}
