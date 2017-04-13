using System.IO;

namespace TeleSharp.TL
{
    [TLObject(-1020139510)]
    public class TLInputGameShortName : TLAbsInputGame
    {
        public override int Constructor => -1020139510;

        public TLAbsInputUser bot_id { get; set; }
        public string short_name { get; set; }


        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            bot_id = (TLAbsInputUser) ObjectUtils.DeserializeObject(br);
            short_name = StringUtil.Deserialize(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(bot_id, bw);
            StringUtil.Serialize(short_name, bw);
        }
    }
}