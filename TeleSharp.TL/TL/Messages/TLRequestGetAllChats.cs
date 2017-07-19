using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-341307408)]
    public class TLRequestGetAllChats : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -341307408;
            }
        }

        public TLVector<int> except_ids { get; set; }
        public Messages.TLAbsChats Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            except_ids = (TLVector<int>)ObjectUtils.DeserializeVector<int>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(except_ids, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLAbsChats)ObjectUtils.DeserializeObject(br);

        }
    }
}
