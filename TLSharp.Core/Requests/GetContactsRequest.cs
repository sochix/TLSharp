using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLSharp.Core.MTProto;
using System.Security.Cryptography;

namespace TLSharp.Core.Requests
{
    public class GetContactsRequest : MTProtoRequest
    {
        private List<int> CurrentContacts { get; set; }

        public List<Contact> Contacts;
        public List<User> Users;

        public GetContactsRequest(IList<int> currentContacts = null)
        {
            if (currentContacts != null)
            {
                CurrentContacts = currentContacts.ToList();
                CurrentContacts.Sort();
            }
        }

        public override void OnSend(BinaryWriter writer)
        {
            writer.Write(0x22c6aa08);
            if (CurrentContacts == null)
                Serializers.String.write(writer, "");
            else
            {
                // create CSV of contactids and calculate md5 hash
                string hash;
                var list = string.Join(",", CurrentContacts);
                using (var md5 = MD5.Create())
                {
                    var retVal = md5.ComputeHash(Encoding.UTF8.GetBytes(list));
                    var sb = new StringBuilder();
                    foreach (var t in retVal)
                    {
                        sb.Append(t.ToString("x2"));
                    }
                    hash = sb.ToString();
                }

                Serializers.String.write(writer, hash);
            }
        }

        public override void OnResponse(BinaryReader reader)
        {
            var code = reader.ReadUInt32();
            // if contactsNotModified then exit
            if (code == 0xb74ba9d2) return;

            reader.ReadInt32(); // vector code
            var contactLen = reader.ReadInt32();
            Contacts = new List<Contact>(contactLen);
            for (var importedIndex = 0; importedIndex < contactLen; importedIndex++)
            {
                var importedElement = TL.Parse<Contact>(reader);
                this.Contacts.Add(importedElement);
            }
            reader.ReadInt32(); // vector code
            var usersLen = reader.ReadInt32();
            Users = new List<User>(usersLen);
            for (var usersIndex = 0; usersIndex < usersLen; usersIndex++)
            {
                var usersElement = TL.Parse<User>(reader);
                this.Users.Add(usersElement);
            }
        }

        public override void OnException(Exception exception)
        {
            throw new NotImplementedException();
        }
        public override bool Confirmed { get { return true; } }
        private readonly bool _responded;
        public override bool Responded { get { return _responded; } }
    }
}
