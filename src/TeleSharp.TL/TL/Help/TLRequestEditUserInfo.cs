using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Help
{
    [TLObject(1723407216)]
    public class TLRequestEditUserInfo : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1723407216;
            }
        }

        public TLAbsInputUser UserId { get; set; }
        public string Message { get; set; }
        public TLVector<TLAbsMessageEntity> Entities { get; set; }
        public Help.TLAbsUserInfo Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            UserId = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
            Message = StringUtil.Deserialize(br);
            Entities = (TLVector<TLAbsMessageEntity>)ObjectUtils.DeserializeVector<TLAbsMessageEntity>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(UserId, bw);
            StringUtil.Serialize(Message, bw);
            ObjectUtils.SerializeObject(Entities, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Help.TLAbsUserInfo)ObjectUtils.DeserializeObject(br);

        }
    }
}
