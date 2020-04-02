using System;

namespace TgSharp.Core.Exceptions
{
    public class CloudPasswordNeededException : Exception
    {
        internal CloudPasswordNeededException(string msg) : base(msg) { }
    }
}