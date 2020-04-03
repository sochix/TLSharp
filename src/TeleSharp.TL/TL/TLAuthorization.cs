using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-1392388579)]
    public class TLAuthorization : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -1392388579;
            }
        }

        public int Flags { get; set; }
        public bool Current { get; set; }
        public bool OfficialApp { get; set; }
        public bool PasswordPending { get; set; }
        public long Hash { get; set; }
        public string DeviceModel { get; set; }
        public string Platform { get; set; }
        public string SystemVersion { get; set; }
        public int ApiId { get; set; }
        public string AppName { get; set; }
        public string AppVersion { get; set; }
        public int DateCreated { get; set; }
        public int DateActive { get; set; }
        public string Ip { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Current = (Flags & 1) != 0;
            OfficialApp = (Flags & 2) != 0;
            PasswordPending = (Flags & 4) != 0;
            Hash = br.ReadInt64();
            DeviceModel = StringUtil.Deserialize(br);
            Platform = StringUtil.Deserialize(br);
            SystemVersion = StringUtil.Deserialize(br);
            ApiId = br.ReadInt32();
            AppName = StringUtil.Deserialize(br);
            AppVersion = StringUtil.Deserialize(br);
            DateCreated = br.ReadInt32();
            DateActive = br.ReadInt32();
            Ip = StringUtil.Deserialize(br);
            Country = StringUtil.Deserialize(br);
            Region = StringUtil.Deserialize(br);

        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);



            bw.Write(Hash);
            StringUtil.Serialize(DeviceModel, bw);
            StringUtil.Serialize(Platform, bw);
            StringUtil.Serialize(SystemVersion, bw);
            bw.Write(ApiId);
            StringUtil.Serialize(AppName, bw);
            StringUtil.Serialize(AppVersion, bw);
            bw.Write(DateCreated);
            bw.Write(DateActive);
            StringUtil.Serialize(Ip, bw);
            StringUtil.Serialize(Country, bw);
            StringUtil.Serialize(Region, bw);

        }
    }
}
