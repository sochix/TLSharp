using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Help
{
    [TLObject(32192344)]
    public class TLUserInfo : TLAbsUserInfo
    {
        public override int Constructor
        {
            get
            {
                return 32192344;
            }
        }

        public string Message { get; set; }
        public TLVector<TLAbsMessageEntity> Entities { get; set; }
        public string Author { get; set; }
        public int Date { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Message = StringUtil.Deserialize(br);
            Entities = (TLVector<TLAbsMessageEntity>)ObjectUtils.DeserializeVector<TLAbsMessageEntity>(br);
            Author = StringUtil.Deserialize(br);
            Date = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            StringUtil.Serialize(Message, bw);
            ObjectUtils.SerializeObject(Entities, bw);
            StringUtil.Serialize(Author, bw);
            bw.Write(Date);

        }
    }
}
