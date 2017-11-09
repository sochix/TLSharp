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

        public int UserId { get; set; }
        public TLAbsContactLink MyLink { get; set; }
        public TLAbsContactLink ForeignLink { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            UserId = br.ReadInt32();
            MyLink = (TLAbsContactLink)ObjectUtils.DeserializeObject(br);
            ForeignLink = (TLAbsContactLink)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(UserId);
            ObjectUtils.SerializeObject(MyLink, bw);
            ObjectUtils.SerializeObject(ForeignLink, bw);

        }
    }
}
