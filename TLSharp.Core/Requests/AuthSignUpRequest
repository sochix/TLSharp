using System;
using System.IO;
using TLSharp.Core.MTProto;

namespace TLSharp.Core.Requests
{
    public class AuthSignUpRequest : MTProtoRequest
    {
        private readonly string _phoneNumber;
        private readonly string _phoneCodeHash;
        private readonly string _code;
        private readonly string _firstName;
        private readonly string _lastName;
        
        public User user;
        public int SessionExpires;

        public AuthSignUpRequest(string phoneNumber, string phoneCodeHash, string code, string firstName, string lastName)
        {
            _phoneNumber = phoneNumber;
            _phoneCodeHash = phoneCodeHash;
            _code = code;
            _firstName = firstName;
            _lastName = lastName; 
        }

        public override void OnSend(BinaryWriter writer)
        {
            writer.Write(0x1b067634);
            Serializers.String.write(writer, _phoneNumber);
            Serializers.String.write(writer, _phoneCodeHash);
            Serializers.String.write(writer, _code);
            Serializers.String.write(writer, _firstName);
            Serializers.String.write(writer, _lastName);
        }

        public override void OnResponse(BinaryReader reader)
        {
            var dataCode = reader.ReadUInt32(); //0xf6b673a4
            var expires = reader.ReadInt32();
            user = TL.Parse<User>(reader);
        }

        public override void OnException(Exception exception)
        {
            throw new NotImplementedException();
        }

        public override bool Confirmed => true;
        public override bool Responded { get; }
    }
}
