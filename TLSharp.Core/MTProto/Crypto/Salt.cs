using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLSharp.Core.MTProto.Crypto
{
    public class Salt : IComparable<Salt>
    {
        private int validSince;
        private int validUntil;
        private ulong salt;

        public Salt(int validSince, int validUntil, ulong salt)
        {
            this.validSince = validSince;
            this.validUntil = validUntil;
            this.salt = salt;
        }

        public int ValidSince
        {
            get { return validSince; }
        }

        public int ValidUntil
        {
            get { return validUntil; }
        }

        public ulong Value
        {
            get { return salt; }
        }

        public int CompareTo(Salt other)
        {
            return validUntil.CompareTo(other.validSince);
        }
    }

    public class SaltCollection
    {
        private SortedSet<Salt> salts;

        public void Add(Salt salt)
        {
            salts.Add(salt);
        }

        public int Count
        {
            get
            {
                return salts.Count;
            }
        }
        // TODO: get actual salt and other...
    }

    public class GetFutureSaltsResponse
    {
        private ulong requestId;
        private int now;
        private SaltCollection salts;

        public GetFutureSaltsResponse(ulong requestId, int now)
        {
            this.requestId = requestId;
            this.now = now;
        }

        public void AddSalt(Salt salt)
        {
            salts.Add(salt);
        }

        public ulong RequestId
        {
            get { return requestId; }
        }

        public int Now
        {
            get { return now; }
        }

        public SaltCollection Salts
        {
            get { return salts; }
        }
    }
}
