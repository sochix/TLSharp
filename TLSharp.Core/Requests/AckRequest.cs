using System;
using System.Collections.Generic;
using System.IO;
using TeleSharp.TL;

namespace TLSharp.Core.Requests
{
    public class AckRequest : TeleSharp.TL.TLMethod
    {
        private readonly List<ulong> _msgs;

        public AckRequest(List<ulong> msgs)
        {
            _msgs = msgs;
        }

        public override void SerializeBody(BinaryWriter writer)
        {
            writer.Write(0x62d6b459); // msgs_ack
            writer.Write(0x1cb5c415); // Vector
            writer.Write(_msgs.Count);
            foreach (ulong messageId in _msgs)
            {
                writer.Write(messageId);
            }
        }

        public override void DeserializeBody(BinaryReader reader)
        {
            throw new NotImplementedException();
        }

        public override void DeserializeResponse(BinaryReader stream)
        {
            throw new NotImplementedException();
        }

        public override bool Confirmed => false;
        public override bool Responded { get; }

        public override int Constructor
        {
            get
            {
                return 0x62d6b459;
            }
        }
    }
}
