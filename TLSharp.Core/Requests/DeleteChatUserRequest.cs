using System;
using System.Collections.Generic;
using System.IO;
using TLSharp.Core.MTProto;

namespace TLSharp.Core.Requests
{
    class DeleteChatUserRequest : MTProtoRequest
    {
        private int _chatID;

        private InputUserContactConstructor _user;
        private string _title;

        
        public Messages_statedMessageConstructor message;
        public DeleteChatUserRequest(int chatID, InputUserContactConstructor user)
        {
            _chatID = chatID;
            _user = user;
        }

        public override void OnSend(BinaryWriter writer)
        {
            writer.Write(0xc3c5cd23);
            writer.Write(_chatID);
            _user.Write(writer);
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
