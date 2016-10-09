using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TeleSharp.TL;
namespace TeleSharp.TL
{
    public abstract class TLMethod : TLObject
    {
        
        public abstract void deserializeResponse(BinaryReader stream);
        #region MTPROTO
        public long MTMessageId { get; set; }
        public int MTSequence { get; set; }
        public bool MTDirty { get; set; }
        public bool MTSended { get; private set; }
        public DateTime MTSendTime { get; private set; }
        public bool MTConfirmReceived { get; set; }
        public virtual bool MTConfirmed { get; } = true;
        public virtual bool MTResponded { get; } = false;

        public virtual void OnSendSuccess()
        {
            MTSendTime = DateTime.Now;
            MTSended = true;
        }

        public virtual void OnConfirm()
        {
            MTConfirmReceived = true;
        }

        public bool NeedResend
        {
            get
            {
                return MTDirty || (MTConfirmed && !MTConfirmReceived && DateTime.Now - MTSendTime > TimeSpan.FromSeconds(3));
            }
        }
        #endregion

    }
}
