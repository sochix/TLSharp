using System;
using System.IO;
using TeleSharp.TL;
namespace TLSharp.Core.Requests
{
    public class DownloadFileRequest : MTProtoRequest
    {
        private GetFileArgs args = new GetFileArgs();
        public TeleSharp.TL.File file;

        public DownloadFileRequest(InputFileLocation loc,int offset=0,int limit=0)
        {
            args.location = loc;
            args.offset = offset;
            args.limit = limit;
        }

        public override void OnSend(BinaryWriter writer)
        {
            Serializer.Serialize(args, typeof(InputFileLocation), writer);
        }

        public override void OnResponse(BinaryReader reader)
        {
            file = (TeleSharp.TL.File)Deserializer.Deserialize(typeof(TeleSharp.TL.File), reader);
        }

        public override void OnException(Exception exception)
        {
            throw new NotImplementedException();
        }

        public override bool Confirmed => true;
        public override bool Responded { get; }
    }
}
