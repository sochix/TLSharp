using System;
using System.Collections.Generic;
using System.IO;
using TLSharp.Core.MTProto;

namespace TLSharp.Core.Requests
{
    public class GetContacts : MTProtoRequest
    {
        public List<Contact> contacts;

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
            var response = reader.ReadInt32();
            if (response == 0xb74ba9d2) //contacts not modified
            {
                contacts = new List<Contact>(0);
            }
            else if (response == 0x6f8b8cb2)
            {
                reader.ReadInt32(); //1cb5c415 vector
                var len = reader.ReadInt32();

                contacts = new List<Contact>(len);
                for (int i = 0; i < len; i++)
                {
                    contacts.Add(TL.Parse<Contact>(reader));
                }
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
