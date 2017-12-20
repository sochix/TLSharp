using System.IO;

namespace TeleSharp.TL.Auth
{
    [TLObject(-2035355412)]
    public class TLRequestSendCode : TLMethod
    {
        public bool AllowFlashcall { get; set; }

        public string ApiHash { get; set; }

        public int ApiId { get; set; }

        public override int Constructor
        {
            get
            {
                return -2035355412;
            }
        }

        public bool? CurrentNumber { get; set; }

        public int Flags { get; set; }

        public string PhoneNumber { get; set; }

        public Auth.TLSentCode Response { get; set; }

        public void ComputeFlags()
        {
        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            AllowFlashcall = (Flags & 1) != 0;
            PhoneNumber = StringUtil.Deserialize(br);
            if ((Flags & 1) != 0)
                CurrentNumber = BoolUtil.Deserialize(br);
            else
                CurrentNumber = null;

            ApiId = br.ReadInt32();
            ApiHash = StringUtil.Deserialize(br);
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            Response = (Auth.TLSentCode)ObjectUtils.DeserializeObject(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);

            StringUtil.Serialize(PhoneNumber, bw);
            if ((Flags & 1) != 0)
                BoolUtil.Serialize(CurrentNumber.Value, bw);
            bw.Write(ApiId);
            StringUtil.Serialize(ApiHash, bw);
        }
    }
}
