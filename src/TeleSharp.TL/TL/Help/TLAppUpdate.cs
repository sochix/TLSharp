using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Help
{
    [TLObject(-1987579119)]
    public class TLAppUpdate : TLAbsAppUpdate
    {
        public override int Constructor
        {
            get
            {
                return -1987579119;
            }
        }

        public int Id { get; set; }
        public bool Critical { get; set; }
        public string Url { get; set; }
        public string Text { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Id = br.ReadInt32();
            Critical = BoolUtil.Deserialize(br);
            Url = StringUtil.Deserialize(br);
            Text = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Id);
            BoolUtil.Serialize(Critical, bw);
            StringUtil.Serialize(Url, bw);
            StringUtil.Serialize(Text, bw);

        }
    }
}
