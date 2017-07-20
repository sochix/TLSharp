using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-106911223)]
    public class TLRequestAddChatUser : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -106911223;
            }
        }

        public int chat_id { get; set; }
        public TLAbsInputUser user_id { get; set; }
        public int fwd_limit { get; set; }
        public TLAbsUpdates Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            chat_id = br.ReadInt32();
            user_id = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
            fwd_limit = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(chat_id);
            ObjectUtils.SerializeObject(user_id, bw);
            bw.Write(fwd_limit);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

        }
    }
}
