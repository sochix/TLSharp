using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-326379039)]
    public class TLRequestToggleChatAdmins : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -326379039;
            }
        }

        public int ChatId { get; set; }
        public bool Enabled { get; set; }
        public TLAbsUpdates Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            ChatId = br.ReadInt32();
            Enabled = BoolUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(ChatId);
            BoolUtil.Serialize(Enabled, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

        }
    }
}
