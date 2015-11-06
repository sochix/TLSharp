using System;
using System.Collections.Generic;
using System.IO;
using TLSharp.Core.MTProto;

namespace TLSharp.Core.Requests
{
    class GetUsersRequest : MTProtoRequest
    {
        List<InputUser> _id;

        public List<User> users;

        public GetUsersRequest(List<InputUser> id)
        {
            _id = id;
        }

        public override void OnSend(BinaryWriter writer)
        {
            writer.Write(0xd91a548);
            writer.Write(0x1cb5c415); // vector#1cb5c415
            writer.Write(_id.Count); // vector length
            foreach (var id in _id)
                id.Write(writer);
        }

        public override void OnResponse(BinaryReader reader)
        {
            var code = reader.ReadUInt32(); // vector#1cb5c415
            int users_len = reader.ReadInt32(); // vector length
            if (users_len != 0)
            {
                users = new List<User>(users_len);
                for (int i = 0; i < users_len; i++)
                    users.Add(TL.Parse<User>(reader));
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
