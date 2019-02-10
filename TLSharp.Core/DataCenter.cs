
namespace TLSharp.Core
{
    internal class DataCenter
    {
        internal DataCenter (int? dcId, string address, int port)
        {
            DataCenterId = dcId;
            Address = address;
            Port = port;
        }

        internal DataCenter (string address, int port) : this (null, address, port)
        {
        }

        internal int? DataCenterId { get; private set; }
        internal string Address { get; private set; }
        internal int Port { get; private set; }
    }
}
