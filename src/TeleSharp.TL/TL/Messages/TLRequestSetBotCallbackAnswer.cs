using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Messages
{
    [TLObject(-712043766)]
    public class TLRequestSetBotCallbackAnswer : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -712043766;
            }
        }

        public int Flags { get; set; }
        public bool Alert { get; set; }
        public long QueryId { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
        public int CacheTime { get; set; }
        public bool Response { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = Alert ? (Flags | 2) : (Flags & ~2);
            Flags = Message != null ? (Flags | 1) : (Flags & ~1);
            Flags = Url != null ? (Flags | 4) : (Flags & ~4);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Alert = (Flags & 2) != 0;
            QueryId = br.ReadInt64();
            if ((Flags & 1) != 0)
                Message = StringUtil.Deserialize(br);
            else
                Message = null;

            if ((Flags & 4) != 0)
                Url = StringUtil.Deserialize(br);
            else
                Url = null;

            CacheTime = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);

            bw.Write(QueryId);
            if ((Flags & 1) != 0)
                StringUtil.Serialize(Message, bw);
            if ((Flags & 4) != 0)
                StringUtil.Serialize(Url, bw);
            bw.Write(CacheTime);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = BoolUtil.Deserialize(br);

        }
    }
}
