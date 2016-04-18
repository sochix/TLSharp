using System;
using System.IO;
using TLSharp.Core.MTProto;

namespace TLSharp.Core.Requests
{
    public class ImportByUserName : MTProtoRequest
    {
        private readonly string _userName;
        public int id { get; private set; }
        public ImportByUserName(string userName)
        {
            _userName = userName;
        }

        public override void OnSend(BinaryWriter writer)
        {
            writer.Write(0xBF0131C);
            Serializers.String.write(writer, _userName);
        }

        public override void OnResponse(BinaryReader reader)
        {
            var code = reader.ReadUInt32();
            id = reader.ReadInt32();
        }

        public override void OnException(Exception exception)
        {
            throw new NotImplementedException();
        }

        public override bool Confirmed => true;
        public override bool Responded { get; }
    }
}