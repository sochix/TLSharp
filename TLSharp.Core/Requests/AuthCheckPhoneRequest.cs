using System;
using System.IO;
using TLSharp.Core.MTProto;

namespace TLSharp.Core.Requests
{
    public class AuthCheckPhoneRequest : MTProtoRequest
    {
        private string _phoneNumber;
        public bool _phoneRegistered;
        private bool _phoneInvited;

        public AuthCheckPhoneRequest(string phoneNumber)
        {
            _phoneNumber = phoneNumber;
        }

        public override void OnSend(BinaryWriter writer)
        {
            writer.Write(0x6fe51dfb);
            Serializers.String.write(writer, _phoneNumber);
        }

        public override void OnResponse(BinaryReader reader)
        {
            var dataCode = reader.ReadUInt32(); // #e300cc3b 
            this._phoneRegistered = reader.ReadUInt32() == 0x997275b5;
            this._phoneInvited = reader.ReadUInt32() == 0x997275b5;
        }

        public override void OnException(Exception exception)
        {
            throw new NotImplementedException();
        }

        public override bool Confirmed => true;
        public override bool Responded { get; }
    }
}
