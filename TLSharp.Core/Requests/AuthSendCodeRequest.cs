using System;
using System.IO;
using TLSharp.Core.MTProto;

namespace TLSharp.Core.Requests
{
    public class AuthSendCodeRequest : MTProtoRequest
    {
        private readonly string _phoneNumber;
        private readonly int _smsType;
        private readonly int _apiId;
        private readonly string _apiHash;
        private readonly string _langCode;
        public bool _phoneRegistered;
        public string _phoneCodeHash;

        public AuthSendCodeRequest(string phoneNumber, int smsType, int apiId, string apiHash, string langCode)
        {
            _phoneNumber = phoneNumber;
            _smsType = smsType;
            _apiId = apiId;
            _apiHash = apiHash;
            _langCode = langCode;
        }

        public override void OnSend(BinaryWriter writer)
        {
            writer.Write(0x768d5f4d);
            Serializers.String.write(writer, _phoneNumber);
            writer.Write(_smsType);
            writer.Write(_apiId);
            Serializers.String.write(writer, _apiHash);
            Serializers.String.write(writer, _langCode);
        }

        public override void OnResponse(BinaryReader reader)
        {
            var boolTrue = 0x997275b5;
            var dataCode = reader.ReadUInt32(); // 0x2215bcbd
            _phoneRegistered = reader.ReadUInt32() == boolTrue;
            _phoneCodeHash = Serializers.String.read(reader);
            var sendCodeTimeout = reader.ReadInt32();
            var isPassword = reader.ReadUInt32() == boolTrue;
        }

        public override void OnException(Exception exception)
        {
            throw new NotImplementedException();
        }

        public override bool Confirmed => true;
        public override bool Responded { get; }
    }
}
