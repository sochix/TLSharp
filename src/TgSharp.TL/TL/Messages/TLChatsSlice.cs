using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL.Messages
{
    [TLObject(-1663561404)]
    public class TLChatsSlice : TLAbsChats
    {
        public override int Constructor
        {
            get
            {
                return -1663561404;
            }
        }

        public int Count { get; set; }
        public TLVector<TLAbsChat> Chats { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Count = br.ReadInt32();
            Chats = (TLVector<TLAbsChat>)ObjectUtils.DeserializeVector<TLAbsChat>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Count);
            ObjectUtils.SerializeObject(Chats, bw);
        }
    }
}
