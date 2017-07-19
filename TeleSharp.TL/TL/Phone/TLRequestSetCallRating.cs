using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Phone
{
    [TLObject(475228724)]
    public class TLRequestSetCallRating : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 475228724;
            }
        }

        public TLInputPhoneCall peer { get; set; }
        public int rating { get; set; }
        public string comment { get; set; }
        public TLAbsUpdates Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            peer = (TLInputPhoneCall)ObjectUtils.DeserializeObject(br);
            rating = br.ReadInt32();
            comment = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(peer, bw);
            bw.Write(rating);
            StringUtil.Serialize(comment, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

        }
    }
}
