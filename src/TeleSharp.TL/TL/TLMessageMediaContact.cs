using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1585262393)]
    public class TLMessageMediaContact : TLAbsMessageMedia
    {
        public override int Constructor
        {
            get
            {
                return 1585262393;
            }
        }

        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserId { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            PhoneNumber = StringUtil.Deserialize(br);
            FirstName = StringUtil.Deserialize(br);
            LastName = StringUtil.Deserialize(br);
            UserId = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(PhoneNumber, bw);
            StringUtil.Serialize(FirstName, bw);
            StringUtil.Serialize(LastName, bw);
            bw.Write(UserId);

        }
    }
}
