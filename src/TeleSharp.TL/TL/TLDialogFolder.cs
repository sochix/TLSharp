using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(1908216652)]
    public class TLDialogFolder : TLAbsDialog
    {
        public override int Constructor
        {
            get
            {
                return 1908216652;
            }
        }

        public int Flags { get; set; }
        public bool Pinned { get; set; }
        public TLFolder Folder { get; set; }
        public TLAbsPeer Peer { get; set; }
        public int TopMessage { get; set; }
        public int UnreadMutedPeersCount { get; set; }
        public int UnreadUnmutedPeersCount { get; set; }
        public int UnreadMutedMessagesCount { get; set; }
        public int UnreadUnmutedMessagesCount { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Pinned = (Flags & 4) != 0;
            Folder = (TLFolder)ObjectUtils.DeserializeObject(br);
            Peer = (TLAbsPeer)ObjectUtils.DeserializeObject(br);
            TopMessage = br.ReadInt32();
            UnreadMutedPeersCount = br.ReadInt32();
            UnreadUnmutedPeersCount = br.ReadInt32();
            UnreadMutedMessagesCount = br.ReadInt32();
            UnreadUnmutedMessagesCount = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);

            ObjectUtils.SerializeObject(Folder, bw);
            ObjectUtils.SerializeObject(Peer, bw);
            bw.Write(TopMessage);
            bw.Write(UnreadMutedPeersCount);
            bw.Write(UnreadUnmutedPeersCount);
            bw.Write(UnreadMutedMessagesCount);
            bw.Write(UnreadUnmutedMessagesCount);

        }
    }
}
