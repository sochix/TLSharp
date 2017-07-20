using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-400399203)]
    public class TLRequestGetGameHighScores : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -400399203;
            }
        }

        public TLAbsInputPeer peer { get; set; }
        public int id { get; set; }
        public TLAbsInputUser user_id { get; set; }
        public Messages.TLHighScores Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            id = br.ReadInt32();
            user_id = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(peer, bw);
            bw.Write(id);
            ObjectUtils.SerializeObject(user_id, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLHighScores)ObjectUtils.DeserializeObject(br);

        }
    }
}
