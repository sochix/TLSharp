using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1831650802)]
    public class TLUrlAuthResultRequest : TLAbsUrlAuthResult
    {
        public override int Constructor
        {
            get
            {
                return -1831650802;
            }
        }

        public int Flags { get; set; }
        public bool RequestWriteAccess { get; set; }
        public TLAbsUser Bot { get; set; }
        public string Domain { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            RequestWriteAccess = (Flags & 1) != 0;
            Bot = (TLAbsUser)ObjectUtils.DeserializeObject(br);
            Domain = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);

            ObjectUtils.SerializeObject(Bot, bw);
            StringUtil.Serialize(Domain, bw);

        }
    }
}
