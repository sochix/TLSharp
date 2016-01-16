using System;
using System.Collections.Generic;
using System.IO;
using TLSharp.Core.MTProto;

namespace TLSharp.Core.Requests
{
    class CreateChatRequest : MTProtoRequest
    {
        private List<InputUserContactConstructor> _id;
        private string _title;

        public Messages_statedMessageConstructor message;
        public CreateChatRequest(List<InputUserContactConstructor> id, string title)
        {
            _id = id;
            _title = title;
        }

        public override void OnSend(BinaryWriter writer)
        {
            writer.Write(0x419d9aee);
            writer.Write(0x1cb5c415); // vector#1cb5c415
            writer.Write(_id.Count); // vector length
            foreach (var id in _id)
                id.Write(writer);
            Serializers.String.write(writer, _title);
        }

        public override void OnResponse(BinaryReader reader)
        {
            message = TL.Parse<Messages_statedMessageConstructor>(reader);
        }

        public override void OnException(Exception exception)
        {
            throw new NotImplementedException();
        }

        public override bool Confirmed { get { return true; } }
        private readonly bool responded;
        public override bool Responded { get { return responded; } }
    }
}
