using System;
using System.Collections.Generic;
using System.IO;
using TLSharp.Core.MTProto;

namespace TLSharp.Core.Requests
{
    public class ImportContactRequest : MTProtoRequest
    {
        private InputContact Contact { get; set; }
        private bool Replace { get; set; }

        public List<ImportedContact> imported;
        public List<User> users;

        public ImportContactRequest(InputContact contact, bool shouldReplace = true)
        {
            Contact = contact;
            Replace = shouldReplace;
        }

        public override void OnSend(BinaryWriter writer)
        {
            writer.Write(0xda30b32d);
            writer.Write(0x1cb5c415);
            writer.Write(1);
            Contact.Write(writer);
            writer.Write(Replace ? 0x997275b5 : 0xbc799737);
        }

        public override void OnResponse(BinaryReader reader)
        {
            var code = reader.ReadUInt32();
            var result = reader.ReadInt32(); // vector code
            int imported_len = reader.ReadInt32();
            this.imported = new List<ImportedContact>(imported_len);
            for (int imported_index = 0; imported_index < imported_len; imported_index++)
            {
                ImportedContact imported_element;
                imported_element = TL.Parse<ImportedContact>(reader);
                this.imported.Add(imported_element);
            }
            reader.ReadInt32(); // vector code
            int users_len = reader.ReadInt32();
            this.users = new List<User>(users_len);
            for (int users_index = 0; users_index < users_len; users_index++)
            {
                User users_element;
                users_element = TL.Parse<User>(reader);
                this.users.Add(users_element);
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
