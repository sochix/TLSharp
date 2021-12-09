using System;
using System.Text;

namespace TLSharp.Core.Network
{
    public static class Sniffer
    {
        public static string MessageOut(byte[] data)
        {
            return WriteMessage(new StringBuilder("[OUT]:"), data);
        }

        public static string MessageIn(byte[] data)
        {
            return WriteMessage(new StringBuilder("[IN]:"), data);
        }

        private static string WriteMessage(StringBuilder log, byte[] data)
        {
            foreach (var b in data)
                log.AppendFormat(" {0:x2}", b);
            return log.ToString();
        }
    }
}
