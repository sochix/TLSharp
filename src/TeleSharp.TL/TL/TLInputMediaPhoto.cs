using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1279654347)]
    public class TLInputMediaPhoto : TLAbsInputMedia
    {
        public override int Constructor
        {
            get
            {
                return -1279654347;
            }
        }

        public int Flags { get; set; }
        public TLAbsInputPhoto Id { get; set; }
        public int? TtlSeconds { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Id = (TLAbsInputPhoto)ObjectUtils.DeserializeObject(br);
            if ((Flags & 1) != 0)
                TtlSeconds = br.ReadInt32();
            else
                TtlSeconds = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            ObjectUtils.SerializeObject(Id, bw);
            if ((Flags & 1) != 0)
                bw.Write(TtlSeconds.Value);

        }
    }
}
