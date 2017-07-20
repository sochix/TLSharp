using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(120753115)]
    public class TLChatForbidden : TLAbsChat
    {
        public override int Constructor
        {
            get
            {
                return 120753115;
            }
        }

        public int id { get; set; }
        public string title { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            id = br.ReadInt32();
            title = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(id);
            StringUtil.Serialize(title, bw);

        }
    }
}
