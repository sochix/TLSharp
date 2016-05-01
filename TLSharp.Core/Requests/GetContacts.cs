using System;
using System.Collections.Generic;
using System.IO;
using TLSharp.Core.MTProto;

namespace TLSharp.Core.Requests
{
    public class GetContacts : MTProtoRequest
    {
        public contacts_Contacts contacts;

        private string contactList;

        public GetContacts(string contactList = "")
        {
            this.contactList = contactList;
        }

        public override void OnSend(BinaryWriter writer)
        {
            writer.Write(0x22c6aa08);
            Serializers.String.write(writer, contactList); //current contact list (ids) in md5 separated by ','
        }

        public override void OnResponse(BinaryReader reader)
        {
            contacts = TL.Parse<contacts_Contacts>(reader);
        }

        public override void OnException(Exception exception)
        {
            throw new NotImplementedException();
        }

        public override bool Confirmed => true;
        public override bool Responded { get; }
    }
}
