using System;

namespace TgSharp.Core.Exceptions
{
    public class MissingApiConfigurationException : Exception
    {
        public const string InfoUrl = "https://github.com/nblockchain/TgSharp#quick-configuration";

        internal MissingApiConfigurationException(string invalidParamName) :
            base($"Your {invalidParamName} setting is missing. Adjust the configuration first, see {InfoUrl}")
        {
        }
    }
}