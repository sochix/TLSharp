namespace TgSharp.Core.Network.Exceptions
{
    internal class FileMigrationException : DataCenterMigrationException
    {
        internal FileMigrationException(int dc)
            : base ($"File located on a different DC: {dc}.", dc)
        {
        }
    }
}