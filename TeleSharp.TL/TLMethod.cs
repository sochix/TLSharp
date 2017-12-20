using System;
using System.IO;

namespace TeleSharp.TL
{
    public abstract class TLMethod : TLObject
    {
        public virtual bool Confirmed { get; } = true;

        public bool ConfirmReceived { get; set; }

        public bool Dirty { get; set; }

        public long MessageId { get; set; }

        public bool NeedResend
        {
            get
            {
                return Dirty || (Confirmed && !ConfirmReceived && DateTime.Now - SendTime > TimeSpan.FromSeconds(3));
            }
        }

        public virtual bool Responded { get; } = false;

        public bool Sended { get; private set; }

        public DateTime SendTime { get; private set; }

        public int Sequence { get; set; }

        public abstract void DeserializeResponse(BinaryReader stream);

        public virtual void OnConfirm()
        {
            ConfirmReceived = true;
        }

        public virtual void OnSendSuccess()
        {
            SendTime = DateTime.Now;
            Sended = true;
        }
    }
}
