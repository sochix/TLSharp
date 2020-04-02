using System;

namespace TgSharp.Core.Exceptions
{
    public class InvalidPhoneCodeException : Exception
    {
        internal InvalidPhoneCodeException(string msg) : base(msg) { }
    }
}