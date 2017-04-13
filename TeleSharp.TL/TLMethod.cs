﻿using System;
using System.IO;

namespace TeleSharp.TL
{
    public abstract class TLMethod : TLObject
    {
        public abstract void deserializeResponse(BinaryReader stream);

        #region MTPROTO

        public long MessageId { get; set; }
        public int Sequence { get; set; }
        public bool Dirty { get; set; }
        public bool Sended { get; private set; }
        public DateTime SendTime { get; private set; }
        public bool ConfirmReceived { get; set; }
        public virtual bool Confirmed { get; } = true;
        public virtual bool Responded { get; } = false;

        public virtual void OnSendSuccess()
        {
            SendTime = DateTime.Now;
            Sended = true;
        }

        public virtual void OnConfirm()
        {
            ConfirmReceived = true;
        }

        public bool NeedResend => Dirty || Confirmed && !ConfirmReceived &&
                                  DateTime.Now - SendTime > TimeSpan.FromSeconds(3);

        #endregion
    }
}