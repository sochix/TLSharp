using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(2018596725)]
    public class TLRequestUpdateProfile : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return 2018596725;
            }
        }

        public int Flags { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string About { get; set; }
        public TLAbsUser Response { get; set; }


        public void ComputeFlags()
        {
            Flags = 0;
            Flags = FirstName != null ? (Flags | 1) : (Flags & ~1);
            Flags = LastName != null ? (Flags | 2) : (Flags & ~2);
            Flags = About != null ? (Flags | 4) : (Flags & ~4);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            if ((Flags & 1) != 0)
                FirstName = StringUtil.Deserialize(br);
            else
                FirstName = null;

            if ((Flags & 2) != 0)
                LastName = StringUtil.Deserialize(br);
            else
                LastName = null;

            if ((Flags & 4) != 0)
                About = StringUtil.Deserialize(br);
            else
                About = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ComputeFlags();
            bw.Write(Flags);
            if ((Flags & 1) != 0)
                StringUtil.Serialize(FirstName, bw);
            if ((Flags & 2) != 0)
                StringUtil.Serialize(LastName, bw);
            if ((Flags & 4) != 0)
                StringUtil.Serialize(About, bw);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLAbsUser)ObjectUtils.DeserializeObject(br);

        }
    }
}
