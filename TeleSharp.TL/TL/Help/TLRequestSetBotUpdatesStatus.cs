using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Help
{
    [TLObject(-333262899)]
    public class TLRequestSetBotUpdatesStatus : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -333262899;
            }
        }

        public int pending_updates_count { get; set; }
        public string message { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            pending_updates_count = br.ReadInt32();
            message = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(pending_updates_count);
            StringUtil.Serialize(message, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
