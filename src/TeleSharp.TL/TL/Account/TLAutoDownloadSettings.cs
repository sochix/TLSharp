using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL.Account
{
    [TLObject(1674235686)]
    public class TLAutoDownloadSettings : TLObject
    {
        public override int Constructor
        {
            get
            {
                return 1674235686;
            }
        }

        public TLAutoDownloadSettings Low { get; set; }
        public TLAutoDownloadSettings Medium { get; set; }
        public TLAutoDownloadSettings High { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Low = (TLAutoDownloadSettings)ObjectUtils.DeserializeObject(br);
            Medium = (TLAutoDownloadSettings)ObjectUtils.DeserializeObject(br);
            High = (TLAutoDownloadSettings)ObjectUtils.DeserializeObject(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            ObjectUtils.SerializeObject(Low, bw);
            ObjectUtils.SerializeObject(Medium, bw);
            ObjectUtils.SerializeObject(High, bw);

        }
    }
}
