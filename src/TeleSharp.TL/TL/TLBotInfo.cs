using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1729618630)]
    public class TLBotInfo : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1729618630;
            }
        }

        public int UserId { get; set; }
        public string Description { get; set; }
        public TLVector<TLBotCommand> Commands { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            UserId = br.ReadInt32();
            Description = StringUtil.Deserialize(br);
            Commands = (TLVector<TLBotCommand>)ObjectUtils.DeserializeVector<TLBotCommand>(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(UserId);
            StringUtil.Serialize(Description, bw);
            ObjectUtils.SerializeObject(Commands, bw);

        }
    }
}
