using System;
using System.IO;
using TLSharp.Core.MTProto;

namespace TLSharp.Core.Requests
{
	public class ImportByUserName : MTProtoRequest
	{
		private readonly string _userName;
		public ImportByUserName(string userName)
		{
			_userName = userName;
		}

		public override void OnSend(BinaryWriter writer)
		{
			writer.Write(0xf93ccba3);
			Serializers.String.write(writer, _userName);
		}

		public override void OnResponse(BinaryReader reader)
		{
			var code = reader.ReadUInt32();
			var peer = TL.Parse<Peer>(reader);
		}

		public override void OnException(Exception exception)
		{
			throw new NotImplementedException();
		}

		public override bool Confirmed { get; }
		public override bool Responded { get; }
	}
}