using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL.Contacts
{
    [TLObject(-2098076769)]
    public class TLRequestGetSaved : TLMethod
    {
        public override int Constructor
        {
            get
            {
                return -2098076769;
            }
        }

        // manual edit: SavedContact -> TLSavedPhoneContact
        public TLVector<TLSavedPhoneContact> Response { get; set; }

        public void ComputeFlags()
        {
            // do nothing
        }

        public override void DeserializeBody(BinaryReader br)
        {
            // do nothing
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            // do nothing else
        }

        public override void DeserializeResponse(BinaryReader br)
        {
            // manual edit: SavedContact -> TLSavedPhoneContact
            Response = (TLVector<TLSavedPhoneContact>)ObjectUtils.DeserializeVector<TLSavedPhoneContact> (br);
        }
    }
}
