using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLSharp.Core.MTProto;
using TLSharp.Core.Utils;

namespace TLSharp.Core.Requests
{
    //messages.sendMedia#a3c85d76 peer:InputPeer media:InputMedia random_id:long = messages.StatedMessage;
    public class Message_SendMediaRequest : MTProtoRequest
    {
        InputPeer inputPeer;
        InputMedia inputMedia;

        public messages_StatedMessage StatedMessage { get; set; }

        public Message_SendMediaRequest(InputPeer inputPeer, InputMedia inputMedia)
        {
            this.inputPeer = inputPeer;
            this.inputMedia = inputMedia;
        }

        public override void OnSend(BinaryWriter writer)
        {
            writer.Write(0xa3c85d76);
            inputPeer.Write(writer);
            inputMedia.Write(writer);
            long random_id = Helpers.GenerateRandomLong();
            writer.Write(random_id);
        }

        public override void OnResponse(BinaryReader reader)
        {

        }

        public override void OnException(Exception exception)
        {
            throw new NotImplementedException();
        }

        public override bool Confirmed => true;
        public override bool Responded { get; }
    }
}
