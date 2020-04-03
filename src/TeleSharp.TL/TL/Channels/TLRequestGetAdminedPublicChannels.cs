using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Channels
{
    [TLObject(-122669393)]
    public class TLRequestGetAdminedPublicChannels : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -122669393;
            }
        }

        public int Flags { get; set; }
        public bool ByLocation { get; set; }
        public bool CheckLimit { get; set; }
        public Messages.TLAbsChats Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            ByLocation = (Flags & 1) != 0;
            CheckLimit = (Flags & 2) != 0;

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);



        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLAbsChats)ObjectUtils.DeserializeObject(br);

        }
    }
}
