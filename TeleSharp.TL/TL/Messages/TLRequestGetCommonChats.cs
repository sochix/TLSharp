using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(218777796)]
    public class TLRequestGetCommonChats : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 218777796;
            }
        }

        public TLAbsInputUser user_id { get; set; }
        public int max_id { get; set; }
        public int limit { get; set; }
        public Messages.TLAbsChats Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            user_id = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
            max_id = br.ReadInt32();
            limit = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(user_id, bw);
            bw.Write(max_id);
            bw.Write(limit);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLAbsChats)ObjectUtils.DeserializeObject(br);

        }
    }
}
