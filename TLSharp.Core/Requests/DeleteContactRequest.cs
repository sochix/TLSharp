using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TLSharp.Core.MTProto;

namespace TLSharp.Core.Requests
{
    class DeleteContactRequest : MTProtoRequest
    {
        public InputUser _user;

        public DeleteContactRequest(InputUser user)
        {
            _user = user;
        }

        public override bool Confirmed
        {
            get { return true; }
        }

        public override bool Responded
        {
            get { return true; }
        }

        public override void OnException(Exception exception)
        {
            throw new NotImplementedException();
        }

        public override void OnResponse(BinaryReader reader)
        { }

        public override void OnSend(BinaryWriter writer)
        {
            writer.Write(0x8e953744);
            _user.Write(writer);
        }
    }
}
