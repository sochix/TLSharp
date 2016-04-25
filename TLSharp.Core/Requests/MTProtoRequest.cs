using System;
using System.IO;

namespace TLSharp.Core.Requests
{
    public abstract class MTProtoRequest
    {
        public MTProtoRequest()
        {
            Sended = false;
        }

        public long MessageId { get; set; }
        public int Sequence { get; set; }

        public bool Dirty { get; set; }

        public bool Sended { get; private set; }
        public DateTime SendTime { get; private set; }
        public bool ConfirmReceived { get; set; }
        public abstract void OnSend(BinaryWriter writer);
        public abstract void OnResponse(BinaryReader reader);
        public abstract void OnException(Exception exception);
        public abstract bool Confirmed { get; }
        public abstract bool Responded { get; }

        public virtual void OnSendSuccess()
        {
            SendTime = DateTime.Now;
            Sended = true;
        }

        public virtual void OnConfirm()
        {
            ConfirmReceived = true;
        }

        public bool NeedResend
        {
            get
            {
                return Dirty || (Confirmed && !ConfirmReceived && DateTime.Now - SendTime > TimeSpan.FromSeconds(3));
            }
        }
    }
}
