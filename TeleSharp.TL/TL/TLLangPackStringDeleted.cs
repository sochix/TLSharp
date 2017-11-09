using System.IO;
namespace TeleSharp.TL
{
    [TLObject(695856818)]
    public class TLLangPackStringDeleted : TLAbsLangPackString
    {
        public override int Constructor
        {
            get
            {
                return 695856818;
            }
        }

        public string key { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            key = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(key, bw);

        }
    }
}
