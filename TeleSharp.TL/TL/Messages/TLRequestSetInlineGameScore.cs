using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(363700068)]
    public class TLRequestSetInlineGameScore : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 363700068;
            }
        }

        public int flags { get; set; }
        public bool edit_message { get; set; }
        public bool force { get; set; }
        public TLInputBotInlineMessageID id { get; set; }
        public TLAbsInputUser user_id { get; set; }
        public int score { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = edit_message ? (flags | 1) : (flags & ~1);
            flags = force ? (flags | 2) : (flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            edit_message = (flags & 1) != 0;
            force = (flags & 2) != 0;
            id = (TLInputBotInlineMessageID)ObjectUtils.DeserializeObject(br);
            user_id = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
            score = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);


            ObjectUtils.SerializeObject(id, bw);
            ObjectUtils.SerializeObject(user_id, bw);
            bw.Write(score);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
