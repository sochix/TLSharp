using System;
using System.Collections.Generic;
using System.IO;
using TLSharp.Core.MTProto;

namespace TLSharp.Core.Requests
{
    public class GetDialogsRequest : MTProtoRequest
    {
        int _offset;
        int _max_id;
        int _limit;

        public int count;
        public List<Dialog> dialogs;
        public List<Message> messages;
        public List<Chat> chats;
        public List<User> users;

        public GetDialogsRequest(int offset, int max_id, int limit)
        {
            _offset = offset;
            _max_id = max_id;
            _limit = limit;
        }

        public override void OnSend(BinaryWriter writer)
        {
            writer.Write(0xeccf1df6);
            writer.Write(_offset);
            writer.Write(_max_id);
            writer.Write(_limit);
        }

        public override void OnResponse(BinaryReader reader)
        {
            bool dialogsSlice = reader.ReadUInt32() == 0x71e094f3; // else dialogs#15ba6c40

            if (dialogsSlice) count = reader.ReadInt32(); // count

            // dialogs
            var result = reader.ReadUInt32(); // vector#1cb5c415
            int dialogs_len = reader.ReadInt32();
            dialogs = new List<Dialog>(dialogs_len);
            for (int dialogs_index = 0; dialogs_index < dialogs_len; dialogs_index++)
            {
                Dialog dialog_element;
                dialog_element = TL.Parse<Dialog>(reader);
                dialogs.Add(dialog_element);
            }
            // messages
            result = reader.ReadUInt32(); // vector#1cb5c415
            int messages_len = reader.ReadInt32();
            messages = new List<Message>(messages_len);
            for (int message_index = 0; message_index < messages_len; message_index++)
            {
                Message messages_element;
                messages_element = TL.Parse<Message>(reader);
                messages.Add(messages_element);
            }
            // chats
            result = reader.ReadUInt32(); // vector#1cb5c415
            int chats_len = reader.ReadInt32();
            chats = new List<Chat>(chats_len);
            for (int chat_index = 0; chat_index < chats_len; chat_index++)
            {
                Chat chats_element;
                chats_element = TL.Parse<Chat>(reader);
                chats.Add(chats_element);
            }
            // users
            result = reader.ReadUInt32(); // vector#1cb5c415
            int users_len = reader.ReadInt32();
            users = new List<User>(users_len);
            for (int users_index = 0; users_index < users_len; users_index++)
            {
                User users_element;
                users_element = TL.Parse<User>(reader);
                users.Add(users_element);
            }
        }

        public override void OnException(Exception exception)
        {
            throw new NotImplementedException();
        }

        public override bool Confirmed => true;
        public override bool Responded { get; }
    }
}
