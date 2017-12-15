using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    //[TLObject(0x708e0195)]
    //public class TLRequestForwardMessages : TLMethod
    //{
    //    // Methods
    //    public void ComputeFlags()
    //    {
    //        this.flags = 0;
    //        this.flags = this.silent ? (this.flags | 0x20) : (this.flags & -33);
    //        this.flags = this.background ? (this.flags | 0x40) : (this.flags & -65);
    //        this.flags = this.with_my_score ? (this.flags | 0x100) : (this.flags & -257);
    //    }

    //    public override void DeserializeBody(BinaryReader br)
    //    {
    //        this.flags = br.ReadInt32();
    //        this.silent = (this.flags & 0x20) > 0;
    //        this.background = (this.flags & 0x40) > 0;
    //        this.with_my_score = (this.flags & 0x100) > 0;
    //        this.from_peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
    //        this.id = ObjectUtils.DeserializeVector<int>(br);
    //        this.random_id = ObjectUtils.DeserializeVector<long>(br);
    //        this.to_peer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
    //    }

    //    public override void DeserializeResponse(BinaryReader br)
    //    {
    //        this.Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);
    //    }

    //    public override void SerializeBody(BinaryWriter bw)
    //    {
    //        bw.Write(this.Constructor);
    //        this.ComputeFlags();
    //        bw.Write(this.flags);
    //        ObjectUtils.SerializeObject(this.from_peer, bw);
    //        ObjectUtils.SerializeObject(this.id, bw);
    //        ObjectUtils.SerializeObject(this.random_id, bw);
    //        ObjectUtils.SerializeObject(this.to_peer, bw);
    //    }

    //    // Properties
    //    public bool background { get; set; }

    //    public override int Constructor
    //    {
    //        get
    //        {
    //            return 0x708e0195;
    //        }
    //    }

    //    public int flags { get; set; }

    //    public TLAbsInputPeer from_peer { get; set; }

    //    public TLVector<int> id { get; set; }

    //    public TLVector<long> random_id { get; set; }

    //    public TLAbsUpdates Response { get; set; }

    //    public bool silent { get; set; }

    //    public TLAbsInputPeer to_peer { get; set; }

    //    public bool with_my_score { get; set; }
    //}



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

        public int Flags { get; set; }
        public bool Silent { get; set; }
        public bool Background { get; set; }
        public bool WithMyScore { get; set; }
        public TLAbsInputPeer FromPeer { get; set; }
        public TLVector<int> Id { get; set; }
        public TLVector<long> RandomId { get; set; }
        public TLAbsInputPeer ToPeer { get; set; }
        public TLAbsUpdates Response { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = Silent ? (Flags | 32) : (Flags & ~32);
            Flags = Background ? (Flags | 64) : (Flags & ~64);
            Flags = WithMyScore ? (Flags | 256) : (Flags & ~256);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Silent = (Flags & 32) != 0;
            Background = (Flags & 64) != 0;
            WithMyScore = (Flags & 256) != 0;
            FromPeer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);
            Id = (TLVector<int>)ObjectUtils.DeserializeVector<int>(br);
            RandomId = (TLVector<long>)ObjectUtils.DeserializeVector<long>(br);
            ToPeer = (TLAbsInputPeer)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);



            ObjectUtils.SerializeObject(FromPeer, bw);
            ObjectUtils.SerializeObject(Id, bw);
            ObjectUtils.SerializeObject(RandomId, bw);
            ObjectUtils.SerializeObject(ToPeer, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUpdates)ObjectUtils.DeserializeObject(br);

        }
    }
}
