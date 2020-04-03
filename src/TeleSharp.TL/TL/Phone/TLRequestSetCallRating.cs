using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Phone
{
    [TLObject(1508562471)]
    public class TLRequestSetCallRating : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1508562471;
            }
        }

        public int Flags { get; set; }
        public bool UserInitiative { get; set; }
        public TLInputPhoneCall Peer { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public TLAbsUpdates Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            UserInitiative = (Flags & 1) != 0;
            Peer = (TLInputPhoneCall)ObjectUtils.DeserializeObject(br);
            Rating = br.ReadInt32();
            Comment = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);

            ObjectUtils.SerializeObject(Peer, bw);
            bw.Write(Rating);
            StringUtil.Serialize(Comment, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

        }
    }
}
