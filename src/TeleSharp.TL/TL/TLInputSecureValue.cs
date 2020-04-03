using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
namespace TeleSharp.TL
{
    [TLObject(-618540889)]
    public class TLInputSecureValue : TLObject
    {
        public override int Constructor
        {
            get
            {
                return -618540889;
            }
        }

        public int Flags { get; set; }
        public TLAbsSecureValueType Type { get; set; }
        public TLSecureData Data { get; set; }
        public TLAbsInputSecureFile FrontSide { get; set; }
        public TLAbsInputSecureFile ReverseSide { get; set; }
        public TLAbsInputSecureFile Selfie { get; set; }
        public TLVector<TLAbsInputSecureFile> Translation { get; set; }
        public TLVector<TLAbsInputSecureFile> Files { get; set; }
        public TLAbsSecurePlainData PlainData { get; set; }


        public void ComputeFlags()
        {

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Type = (TLAbsSecureValueType)ObjectUtils.DeserializeObject(br);
            if ((Flags & 1) != 0)
                Data = (TLSecureData)ObjectUtils.DeserializeObject(br);
            else
                Data = null;

            if ((Flags & 2) != 0)
                FrontSide = (TLAbsInputSecureFile)ObjectUtils.DeserializeObject(br);
            else
                FrontSide = null;

            if ((Flags & 4) != 0)
                ReverseSide = (TLAbsInputSecureFile)ObjectUtils.DeserializeObject(br);
            else
                ReverseSide = null;

            if ((Flags & 8) != 0)
                Selfie = (TLAbsInputSecureFile)ObjectUtils.DeserializeObject(br);
            else
                Selfie = null;

            if ((Flags & 64) != 0)
                Translation = (TLVector<TLAbsInputSecureFile>)ObjectUtils.DeserializeVector<TLAbsInputSecureFile>(br);
            else
                Translation = null;

            if ((Flags & 16) != 0)
                Files = (TLVector<TLAbsInputSecureFile>)ObjectUtils.DeserializeVector<TLAbsInputSecureFile>(br);
            else
                Files = null;

            if ((Flags & 32) != 0)
                PlainData = (TLAbsSecurePlainData)ObjectUtils.DeserializeObject(br);
            else
                PlainData = null;


        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            ObjectUtils.SerializeObject(Type, bw);
            if ((Flags & 1) != 0)
                ObjectUtils.SerializeObject(Data, bw);
            if ((Flags & 2) != 0)
                ObjectUtils.SerializeObject(FrontSide, bw);
            if ((Flags & 4) != 0)
                ObjectUtils.SerializeObject(ReverseSide, bw);
            if ((Flags & 8) != 0)
                ObjectUtils.SerializeObject(Selfie, bw);
            if ((Flags & 64) != 0)
                ObjectUtils.SerializeObject(Translation, bw);
            if ((Flags & 16) != 0)
                ObjectUtils.SerializeObject(Files, bw);
            if ((Flags & 32) != 0)
                ObjectUtils.SerializeObject(PlainData, bw);

        }
    }
}
