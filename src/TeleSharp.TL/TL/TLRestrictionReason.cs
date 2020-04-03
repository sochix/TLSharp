using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-797791052)]
    public class TLRestrictionReason : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -797791052;
            }
        }

        public string Platform { get; set; }
        public string Reason { get; set; }
        public string Text { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Platform = StringUtil.Deserialize(br);
            Reason = StringUtil.Deserialize(br);
            Text = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Platform, bw);
            StringUtil.Serialize(Reason, bw);
            StringUtil.Serialize(Text, bw);

        }
    }
}
