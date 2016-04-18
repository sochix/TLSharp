using System;

namespace TLSharp.Core.MTProto.Crypto
{
    public class FactorizedPair
    {
        private readonly BigInteger p;
        private readonly BigInteger q;

        public FactorizedPair(BigInteger p, BigInteger q)
        {
            this.p = p;
            this.q = q;
        }

        public FactorizedPair(long p, long q)
        {
            this.p = BigInteger.ValueOf(p);
            this.q = BigInteger.ValueOf(q);
        }

        public BigInteger Min
        {
            get
            {
                return p.Min(q);
            }
        }

        public BigInteger Max
        {
            get
            {
                return p.Max(q);
            }
        }

        public override string ToString()
        {
            return string.Format("P: {0}, Q: {1}", p, q);
        }
    }
    public class Factorizator
    {
        public static Random random = new Random();
        public static long findSmallMultiplierLopatin(long what)
        {
            long g = 0;
            for (int i = 0; i < 3; i++)
            {
                int q = (random.Next(128) & 15) + 17;
                long x = random.Next(1000000000) + 1, y = x;
                int lim = 1 << (i + 18);
                for (int j = 1; j < lim; j++)
                {
                    long a = x, b = x, c = q;
                    while (b != 0)
                    {
                        if ((b & 1) != 0)
                        {
                            c += a;
                            if (c >= what)
                            {
                                c -= what;
                            }
                        }
                        a += a;
                        if (a >= what)
                        {
                            a -= what;
                        }
                        b >>= 1;
                    }
                    x = c;
                    long z = x < y ? y - x : x - y;
                    g = GCD(z, what);
                    if (g != 1)
                    {
                        break;
                    }
                    if ((j & (j - 1)) == 0)
                    {
                        y = x;
                    }
                }
                if (g > 1)
                {
                    break;
                }
            }

            long p = what / g;
            return Math.Min(p, g);
        }

        public static long GCD(long a, long b)
        {
            while (a != 0 && b != 0)
            {
                while ((b & 1) == 0)
                {
                    b >>= 1;
                }
                while ((a & 1) == 0)
                {
                    a >>= 1;
                }
                if (a > b)
                {
                    a -= b;
                }
                else {
                    b -= a;
                }
            }
            return b == 0 ? a : b;
        }

        public static FactorizedPair Factorize(BigInteger pq)
        {
            if (pq.BitLength < 64)
            {
                long pqlong = pq.LongValue;
                long divisor = findSmallMultiplierLopatin(pqlong);
                return new FactorizedPair(BigInteger.ValueOf(divisor), BigInteger.ValueOf(pqlong / divisor));
            }
            else {
                // TODO: port pollard factorization
                throw new InvalidOperationException("pq too long; TODO: port the pollard algo");
                // logger.error("pq too long; TODO: port the pollard algo");
                // return null;
            }
        }

    }


}
