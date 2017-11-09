using System.IO;
namespace TeleSharp.TL
{
    [TLObject(1816636575)]
    public class TLLangPackStringPluralized : TLAbsLangPackString
    {
        public override int Constructor
        {
            get
            {
                return 1816636575;
            }
        }

        public int flags { get; set; }
        public string key { get; set; }
        public string zero_value { get; set; }
        public string one_value { get; set; }
        public string two_value { get; set; }
        public string few_value { get; set; }
        public string many_value { get; set; }
        public string other_value { get; set; }


        public void ComputeFlags()
        {
            flags = 0;
            flags = zero_value != null ? (flags | 1) : (flags & ~1);
            flags = one_value != null ? (flags | 2) : (flags & ~2);
            flags = two_value != null ? (flags | 4) : (flags & ~4);
            flags = few_value != null ? (flags | 8) : (flags & ~8);
            flags = many_value != null ? (flags | 16) : (flags & ~16);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            flags = br.ReadInt32();
            key = StringUtil.Deserialize(br);
            if ((flags & 1) != 0)
                zero_value = StringUtil.Deserialize(br);
            else
                zero_value = null;

            if ((flags & 2) != 0)
                one_value = StringUtil.Deserialize(br);
            else
                one_value = null;

            if ((flags & 4) != 0)
                two_value = StringUtil.Deserialize(br);
            else
                two_value = null;

            if ((flags & 8) != 0)
                few_value = StringUtil.Deserialize(br);
            else
                few_value = null;

            if ((flags & 16) != 0)
                many_value = StringUtil.Deserialize(br);
            else
                many_value = null;

            other_value = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(flags);
            StringUtil.Serialize(key, bw);
            if ((flags & 1) != 0)
                StringUtil.Serialize(zero_value, bw);
            if ((flags & 2) != 0)
                StringUtil.Serialize(one_value, bw);
            if ((flags & 4) != 0)
                StringUtil.Serialize(two_value, bw);
            if ((flags & 8) != 0)
                StringUtil.Serialize(few_value, bw);
            if ((flags & 16) != 0)
                StringUtil.Serialize(many_value, bw);
            StringUtil.Serialize(other_value, bw);

        }
    }
}
