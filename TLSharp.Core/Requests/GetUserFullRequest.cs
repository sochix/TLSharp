using System;
using System.IO;
using TLSharp.Core.MTProto;

namespace TLSharp.Core.Requests
{
    public class GetUserFullRequest : MTProtoRequest
    {
        private InputUser _inputUser;
        public UserFull _userFull;

        public GetUserFullRequest(int id)
        {
            _inputUser = new InputUserContactConstructor(id);
        }

        public override void OnSend(BinaryWriter writer)
        {
            writer.Write(0xca30a5b1);
            _inputUser.Write(writer);
        }

        public override void OnResponse(BinaryReader reader)
        {
            _userFull = new UserFullConstructor();
            var dataCode = reader.ReadUInt32();
            _userFull.Read(reader);
        }

        public override void OnException(Exception exception)
        {
            throw new NotImplementedException();
        }

        public override bool Responded
        {
            get;
        }

        public override bool Confirmed => true;
    }
}
