using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace TLSharp.Core.Network
{
	public class TcpTransport : IDisposable
	{
		private const string defaultConnectionAddress = "91.108.56.165";
		private const int defaultConnectionPort = 443;
		private readonly TcpClient _tcpClient;
		private int sendCounter = 0;

		public TcpTransport(string address = defaultConnectionAddress, int port = defaultConnectionPort)
		{
			_tcpClient = new TcpClient();
			
			var ipAddress = IPAddress.Parse(address);
			_tcpClient.Connect(ipAddress, port);
		}

		public async Task Send(byte[] packet)
		{
			if (!_tcpClient.Connected)
				throw new InvalidOperationException("Client not connected to server.");

			var tcpMessage = new TcpMessage(sendCounter, packet);

			await _tcpClient.GetStream().WriteAsync(tcpMessage.Encode(), 0, tcpMessage.Encode().Length);
			sendCounter++;
		}

		public async Task<TcpMessage> Receieve()
		{
			var buffer = new byte[_tcpClient.ReceiveBufferSize];
			var availableBytes = await _tcpClient.GetStream().ReadAsync(buffer, 0, buffer.Length);

			var result = buffer.Take(availableBytes).ToArray();

			return TcpMessage.Decode(result);
		}

		public void Dispose()
		{
			if (_tcpClient.Connected)
				_tcpClient.Close();
		}
	}
}
