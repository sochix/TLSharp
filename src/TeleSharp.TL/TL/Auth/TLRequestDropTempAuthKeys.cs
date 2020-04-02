using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Auth
{
    [TLObject(-1907842680)]
    public class TLRequestDropTempAuthKeys : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1907842680;
            }
        }

        public TLVector<long> ExceptAuthKeys { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            ExceptAuthKeys = (TLVector<long>)ObjectUtils.DeserializeVector<long>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(ExceptAuthKeys, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
