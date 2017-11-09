using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1868808300)]
    public class TLPaymentRequestedInfo : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1868808300;
            }
        }

        public int Flags { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public TLPostAddress ShippingAddress { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = Name != null ? (Flags | 1) : (Flags & ~1);
            Flags = Phone != null ? (Flags | 2) : (Flags & ~2);
            Flags = Email != null ? (Flags | 4) : (Flags & ~4);
            Flags = ShippingAddress != null ? (Flags | 8) : (Flags & ~8);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            if ((Flags & 1) != 0)
                Name = StringUtil.Deserialize(br);
            else
                Name = null;

            if ((Flags & 2) != 0)
                Phone = StringUtil.Deserialize(br);
            else
                Phone = null;

            if ((Flags & 4) != 0)
                Email = StringUtil.Deserialize(br);
            else
                Email = null;

            if ((Flags & 8) != 0)
                ShippingAddress = (TLPostAddress)ObjectUtils.DeserializeObject(br);
            else
                ShippingAddress = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);
            if ((Flags & 1) != 0)
                StringUtil.Serialize(Name, bw);
            if ((Flags & 2) != 0)
                StringUtil.Serialize(Phone, bw);
            if ((Flags & 4) != 0)
                StringUtil.Serialize(Email, bw);
            if ((Flags & 8) != 0)
                ObjectUtils.SerializeObject(ShippingAddress, bw);

        }
    }
}
