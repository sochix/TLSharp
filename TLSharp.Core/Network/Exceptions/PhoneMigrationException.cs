namespace TLSharp.Core.Network.Exceptions
{
    internal class PhoneMigrationException : DataCenterMigrationException
    {
        internal PhoneMigrationException(int dc)
            : base ($"Phone number registered to a different DC: {dc}.", dc)
        {
        }
    }
}