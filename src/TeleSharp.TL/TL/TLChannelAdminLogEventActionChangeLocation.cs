using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(241923758)]
    public class TLChannelAdminLogEventActionChangeLocation : TLAbsChannelAdminLogEventAction
    {
        public override int Constructor
        {
            get
            {
                return 241923758;
            }
        }

        public TLAbsChannelLocation PrevValue { get; set; }
        public TLAbsChannelLocation NewValue { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            PrevValue = (TLAbsChannelLocation)ObjectUtils.DeserializeObject(br);
            NewValue = (TLAbsChannelLocation)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(PrevValue, bw);
            ObjectUtils.SerializeObject(NewValue, bw);

        }
    }
}
