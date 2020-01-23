using System;

namespace TLSharp.Core.Exceptions
{
    public class CloudPasswordNeededException : Exception
    {
        internal CloudPasswordNeededException(string msg) : base(msg) { }
    }
}