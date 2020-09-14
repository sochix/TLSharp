namespace TgSharp.Core
{
    internal class DataCenter
    {
        private const string defaultConnectionAddress = "149.154.175.100";//"149.154.167.50";
        private const int defaultConnectionPort = 443;

        internal DataCenter (int? dcId, string address = defaultConnectionAddress, int port = defaultConnectionPort)
        {
            DataCenterId = dcId;
            Address = address;
            Port = port;
        }

        internal DataCenter (string address = defaultConnectionAddress, int port = defaultConnectionPort) : this (null, address, port)
        {
        }

        internal int? DataCenterId { get; }
        internal string Address { get; }
        internal int Port { get; }
    }
}