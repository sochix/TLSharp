namespace TLSharp.Core.Network.Exceptions
{
    internal class NetworkMigrationException : DataCenterMigrationException
    {
        internal NetworkMigrationException(int dc)
            : base($"Network located on a different DC: {dc}.", dc)
        {
        }
    }
}