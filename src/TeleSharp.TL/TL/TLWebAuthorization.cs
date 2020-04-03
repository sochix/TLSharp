using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-892779534)]
    public class TLWebAuthorization : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -892779534;
            }
        }

        public long Hash { get; set; }
        public int BotId { get; set; }
        public string Domain { get; set; }
        public string Browser { get; set; }
        public string Platform { get; set; }
        public int DateCreated { get; set; }
        public int DateActive { get; set; }
        public string Ip { get; set; }
        public string Region { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Hash = br.ReadInt64();
            BotId = br.ReadInt32();
            Domain = StringUtil.Deserialize(br);
            Browser = StringUtil.Deserialize(br);
            Platform = StringUtil.Deserialize(br);
            DateCreated = br.ReadInt32();
            DateActive = br.ReadInt32();
            Ip = StringUtil.Deserialize(br);
            Region = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Hash);
            bw.Write(BotId);
            StringUtil.Serialize(Domain, bw);
            StringUtil.Serialize(Browser, bw);
            StringUtil.Serialize(Platform, bw);
            bw.Write(DateCreated);
            bw.Write(DateActive);
            StringUtil.Serialize(Ip, bw);
            StringUtil.Serialize(Region, bw);

        }
    }
}
