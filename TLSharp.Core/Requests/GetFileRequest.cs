using System;
using System.IO;
using TLSharp.Core.MTProto;

namespace TLSharp.Core.Requests
{
    class GetFileRequest : MTProtoRequest
    {
        InputFileLocation _location;
        int _offset;
        int _limit;

        public storage_FileType type;
        public int mtime;
        public byte[] bytes;

        public GetFileRequest(InputFileLocation location, int offset, int limit)
        {
            _location = location;
            _offset = offset;
            _limit = limit;
        }

        public override void OnSend(BinaryWriter writer)
        {
            writer.Write(0xe3a6cfb5);
            _location.Write(writer);
            writer.Write(_offset);
            writer.Write(_limit);
        }

        public override void OnResponse(BinaryReader reader)
        {
            var code = reader.ReadUInt32(); // upload.file#96a18d5

            type = TL.Parse<storage_FileType>(reader);
            mtime = reader.ReadInt32();
            bytes = reader.ReadBytes(_limit);
        }

        public override void OnException(Exception exception)
        {
            throw new NotImplementedException();
        }

        public override bool Confirmed => true;
        public override bool Responded { get; }
    }
}
