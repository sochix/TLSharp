using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(1888354709)]
    public class TLRequestForwardMessages : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 1888354709;
            }
        }

        public int flags { get; set; }
        public bool silent { get; set; }
        public bool background { get; set; }
        public bool with_my_score { get; set; }
        public TLAbsInputPeer from_peer { get; set; }
        public TLVector<int> id { get; set; }
        public TLVector<long> random_id { get; set; }
        public TLAbsInputPeer to_peer { get; set; }
        public TLAbsUpdates Response { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = silent ? (flags | 32) : (flags & ~32);
            flags = background ? (flags | 64) : (flags & ~64);
            flags = with_my_score ? (flags | 256) : (flags & ~256);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            silent = (flags & 32) != 0;
            background = (flags & 64) != 0;
            with_my_score = (flags & 256) != 0;
            from_peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            id = (TLVector<int>)ObjectUtils.DeserializeVector<int>(br);
            random_id = (TLVector<long>)ObjectUtils.DeserializeVector<long>(br);
            to_peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);



            ObjectUtils.SerializeObject(from_peer, bw);
            ObjectUtils.SerializeObject(id, bw);
            ObjectUtils.SerializeObject(random_id, bw);
            ObjectUtils.SerializeObject(to_peer, bw);

        }
        public override void deserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

        }
    }
}
