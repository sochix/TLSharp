using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(-262453244)]
    public class TLRequestInitTakeoutSession : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -262453244;
            }
        }

        public int Flags { get; set; }
        public bool Contacts { get; set; }
        public bool MessageUsers { get; set; }
        public bool MessageChats { get; set; }
        public bool MessageMegagroups { get; set; }
        public bool MessageChannels { get; set; }
        public bool Files { get; set; }
        public int? FileMaxSize { get; set; }
        public Account.TLTakeout Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Contacts = (Flags & 1) != 0;
            MessageUsers = (Flags & 2) != 0;
            MessageChats = (Flags & 4) != 0;
            MessageMegagroups = (Flags & 8) != 0;
            MessageChannels = (Flags & 16) != 0;
            Files = (Flags & 32) != 0;
            if ((Flags & 32) != 0)
                FileMaxSize = br.ReadInt32();
            else
                FileMaxSize = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);






            if ((Flags & 32) != 0)
                bw.Write(FileMaxSize.Value);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Account.TLTakeout)ObjectUtils.DeserializeObject(br);

        }
    }
}
