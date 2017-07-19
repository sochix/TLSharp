using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-1896289088)]
    public class TLRequestSetGameScore : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -1896289088;
            }
        }

        public int flags { get; set; }
        public bool edit_message { get; set; }
        public bool force { get; set; }
        public TLAbsInputPeer peer { get; set; }
        public int id { get; set; }
        public TLAbsInputUser user_id { get; set; }
        public int score { get; set; }
        public TLAbsUpdates Response { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = edit_message ? (flags | 1) : (flags & ~1);
            flags = force ? (flags | 2) : (flags & ~2);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            edit_message = (flags & 1) != 0;
            force = (flags & 2) != 0;
            peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            id = br.ReadInt32();
            user_id = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
            score = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);


            ObjectUtils.SerializeObject(peer, bw);
            bw.Write(id);
            ObjectUtils.SerializeObject(user_id, bw);
            bw.Write(score);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

        }
    }
}
