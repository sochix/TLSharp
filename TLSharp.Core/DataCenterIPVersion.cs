namespace TLSharp.Core
{
    /// <summary>
    /// When the Telegram server responds with a set of addresses to connect to, DataCenterIPVersion indicates a preference 
    /// for how to choose the IP address to connect to 
    /// </summary>
    public enum DataCenterIPVersion
    {
        /// <summary>
        /// Prefers IPv6 addresses if any is passed by Telegram
        /// </summary>
        Default = 0,
        /// <summary>
        /// Takes only IPv4 addresses
        /// </summary>
        OnlyIPv4 = 1,
        /// <summary>
        /// Takes only IPv6 addresses
        /// </summary>
        OnlyIPv6 = 2,
        /// <summary>
        /// Connection to IPv4 addresses is preferred to IPv6 addresses
        /// </summary>
        PreferIPv4 = 3,
        /// <summary>
        /// Connection to IPv6 addresses is preferred to IPv4 addresses 
        /// </summary>
        PreferIPv6 = 4,
    }
}
