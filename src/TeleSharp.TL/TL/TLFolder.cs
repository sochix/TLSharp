using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-11252123)]
    public class TLFolder : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -11252123;
            }
        }

        public int Flags { get; set; }
        public bool AutofillNewBroadcasts { get; set; }
        public bool AutofillPublicGroups { get; set; }
        public bool AutofillNewCorrespondents { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public TLAbsChatPhoto Photo { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            AutofillNewBroadcasts = (Flags & 1) != 0;
            AutofillPublicGroups = (Flags & 2) != 0;
            AutofillNewCorrespondents = (Flags & 4) != 0;
            Id = br.ReadInt32();
            Title = StringUtil.Deserialize(br);
            if ((Flags & 8) != 0)
                Photo = (TLAbsChatPhoto)ObjectUtils.DeserializeObject(br);
            else
                Photo = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);



            bw.Write(Id);
            StringUtil.Serialize(Title, bw);
            if ((Flags & 8) != 0)
                ObjectUtils.SerializeObject(Photo, bw);

        }
    }
}
