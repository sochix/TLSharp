using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Payments
{
    [TLObject(-784000893)]
    public class TLValidatedRequestedInfo : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -784000893;
            }
        }

        public int Flags { get; set; }
        public string Id { get; set; }
        public TLVector<TLShippingOption> ShippingOptions { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = Id != null ? (Flags | 1) : (Flags & ~1);
            Flags = ShippingOptions != null ? (Flags | 2) : (Flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            if ((Flags & 1) != 0)
                Id = StringUtil.Deserialize(br);
            else
                Id = null;

            if ((Flags & 2) != 0)
                ShippingOptions = (TLVector<TLShippingOption>)ObjectUtils.DeserializeVector<TLShippingOption>(br);
            else
                ShippingOptions = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);
            if ((Flags & 1) != 0)
                StringUtil.Serialize(Id, bw);
            if ((Flags & 2) != 0)
                ObjectUtils.SerializeObject(ShippingOptions, bw);

        }
    }
}
