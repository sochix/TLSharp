using System;
using System.Collections.Generic;
using System.IO;
using TLSharp.Core.MTProto;

namespace TLSharp.Core.Requests
{
    class GetHistoryRequest : MTProtoRequest
    {
        InputPeer _peer;
        int _offset;
        int _max_id;
        int _limit;

        public List<Message> messages;
        public List<Chat> chats;
        public List<User> users;

        public GetHistoryRequest(InputPeer peer, int offset, int max_id, int limit)
        {
            _peer = peer;
            _offset = offset;
            _max_id = max_id;
            _limit = limit;
        }

        public override void OnSend(BinaryWriter writer)
        {
            writer.Write(0x92a1df2f);
            _peer.Write(writer);
            writer.Write(_offset);
            writer.Write(_max_id);
            writer.Write(_limit);
        }

        public override void OnResponse(BinaryReader reader)
        {
            bool messagesSlice = reader.ReadUInt32() == 0xb446ae3; // else messages#8c718e87

            if (messagesSlice) reader.ReadInt32(); // count

            // messages
            var result = reader.ReadUInt32(); // vector#1cb5c415
            int messages_len = reader.ReadInt32();
            messages = new List<Message>(messages_len);
            for (var i = 0; i < messages_len; i++)
            {
                var msgEl = TL.Parse<Message>(reader);

                messages.Add(msgEl);
            }

            // chats
            reader.ReadUInt32();
            int chats_len = reader.ReadInt32();
            chats = new List<Chat>(chats_len);
            for (int i = 0; i < chats_len; i++)
                chats.Add(TL.Parse<Chat>(reader));

            /*
			// users
			reader.ReadUInt32();
			int users_len = reader.ReadInt32();
			users = new List<User>(users_len);
			for (int i = 0; i < users_len; i++)
				users.Add(TL.Parse<User>(reader));
			*/
        }

        public override void OnException(Exception exception)
        {
            throw new NotImplementedException();
        }

        public override bool Confirmed => true;
        public override bool Responded { get; }
    }
}
