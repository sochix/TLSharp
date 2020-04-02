using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Channels
{
    [TLObject(-934882771)]
    public class TLRequestExportMessageLink : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -934882771;
            }
        }

        public TLAbsInputChannel Channel { get; set; }
        public int Id { get; set; }
        public TLExportedMessageLink Response { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Channel = (TLAbsInputChannel)ObjectUtils.DeserializeObject(br);
            Id = br.ReadInt32();

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Channel, bw);
            bw.Write(Id);

        }
        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (TLExportedMessageLink)ObjectUtils.DeserializeObject(br);

        }
    }
}
