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

        public TLAbsInputUser UserId { get; set; }
        public int MaxId { get; set; }
        public int Limit { get; set; }
        public Messages.TLAbsChats Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            UserId = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
            MaxId = br.ReadInt32();
            Limit = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(UserId, bw);
            bw.Write(MaxId);
            bw.Write(Limit);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLAbsChats)ObjectUtils.DeserializeObject(br);

        }
    }
}
