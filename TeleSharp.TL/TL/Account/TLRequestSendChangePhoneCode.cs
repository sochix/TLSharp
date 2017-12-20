using System.IO;

namespace TeleSharp.TL.Account
{
    [TLObject(149257707)]
    public class TLRequestSendChangePhoneCode : TLMethod
    {
        public bool AllowFlashcall { get; set; }

        public override int Constructor
        {
            get
            {
                return 149257707;
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
        }
    }
}
