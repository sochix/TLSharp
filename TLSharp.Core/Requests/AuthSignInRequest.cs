using System;
using System.IO;
using TeleSharp.TL;
namespace TLSharp.Core.Requests
{
    public class AuthSignInRequest : MTProtoRequest
    {
        private SignInArgs args = new SignInArgs();
        public IUser user;
        public int SessionExpires;

        public AuthSignInRequest(string phoneNumber, string phoneCodeHash, string code)
        {
            args.phone_number = phoneNumber;
            args.phone_code_hash = phoneCodeHash;
            args.phone_code = code;
        }

        public override void OnSend(BinaryWriter writer)
        {
            Serializer.Serialize(args, typeof(SignInArgs), writer);
        }

        public override void OnResponse(BinaryReader reader)
        {
            Deserializer.Deserialize(typeof(Config), reader);
        }

        public override void OnException(Exception exception)
        {
            throw new NotImplementedException();
        }

        public override bool Confirmed => true;
        public override bool Responded { get; }
    }
}
